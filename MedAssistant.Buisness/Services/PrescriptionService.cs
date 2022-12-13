using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Buisness.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public PrescriptionService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAccountService accountService,
            IUserService userService)
        { 
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.accountService = accountService;
            this.userService = userService;
        }


        public async Task<List<MedicineDTO>> GetAllMedicinesAsync()
        {
            try
            {
                List<MedicineDTO> list = new();

                list.AddRange(unitOfWork.Medicines.Get().Select(x => mapper.Map<MedicineDTO>(x)).AsParallel().ToList());
                return list;
                 
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public int FindMedecineIdByName(string name)
        { 
            var id = unitOfWork.Medicines.FindBy(x=>x.Name.Equals(name)).FirstOrDefault().Id;
            return id;
        }

        public async Task<List<PrescriptionDTO>> GetPrescriptionsbyUserEmailAsync(string email)
        {
            try
            {
                var accountid = await accountService.GetIdAccountByEmailAsync(email);
                var user = await userService.GetUsersByAccountId(accountid);

                var dtos = await unitOfWork.Prescription.Get()
                    .Where(x => x.UserId.Equals(user.Id))
                    .Include(x => x.Medicine)
                    .ToListAsync();

                return dtos.Select(x => mapper.Map<PrescriptionDTO>(x)).ToList();
            }
            catch (Exception)
            { 
                throw;
            } 
        }


        public async Task<int> GetUserIdByEmailAdressAsync(string email)
        {
            try
            {
                var idaccount = await accountService.GetIdAccountByEmailAsync(email);
                return userService.GetUsersByAccountId(idaccount).Result.Id;
            }
            catch (Exception)
            { 
                throw;
            } 
        }


        public async Task<int> CreatePrescriptionAsync(PrescriptionDTO prescriptionDTO)
        {
            try
            {
                await unitOfWork.Prescription.AddAsync(mapper.Map<Prescription>(prescriptionDTO));
                return await unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            } 
        }


        public async Task<PrescriptionDTO> GetPrescriptionByIdAsync(int id)
        {
            try
            {
                var dto = await unitOfWork.Prescription.FindBy(x => x.Id.Equals(id), x => x.Medicine).FirstOrDefaultAsync();
                return mapper.Map<PrescriptionDTO>(dto);
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public Task<int> UpdatePrescriptionAsync(PrescriptionDTO dto)
        {
            try
            {
                unitOfWork.Prescription.Update(mapper.Map<Prescription>(dto));
                return unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task<int> RemovePrescriptionAsync(int id)
        {
            try
            {
                var entity = await unitOfWork.Prescription.GetByIdAsync(id);

                if (entity != null)
                {
                    unitOfWork.Prescription.Remove(entity);
                }
                return await unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            }   
        }

    }
}
