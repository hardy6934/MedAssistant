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
    public class VaccinationService : IVaccinationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public VaccinationService(IUnitOfWork unitOfWork, IMapper mapper, 
            IAccountService accountService, IUserService userService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.accountService = accountService;
            this.userService = userService;
        }



        public Task<int> CreateVaccinationAsync(VaccinationDTO dto)
        {
            unitOfWork.Vaccination.AddAsync(mapper.Map<Vaccination>(dto));
            return unitOfWork.Commit();
        }

        public async Task<VaccinationDTO> GetVaccinationByIdAsync(int id)
        {
           var dto = await unitOfWork.Vaccination.FindBy(x => x.Id.Equals(id), x => x.VaccinationType).FirstOrDefaultAsync();
            return mapper.Map<VaccinationDTO>(dto);
        }


        public async Task<List<VaccinationDTO>> GetVaccinationsbyUserEmailAsync(string UserEmail)
        {

            List<VaccinationDTO> vaccinationDTOs = new List<VaccinationDTO>();

            var accountid = await accountService.GetIdAccountByEmailAsync(UserEmail);
            var user = await userService.GetUsersByAccountId(accountid);
              
            var dtos = await unitOfWork.Vaccination.FindBy(x=>x.UserId.Equals(user.Id),x=>x.VaccinationType).ToListAsync();

            foreach (var n in dtos)
            {
                vaccinationDTOs.Add(mapper.Map<VaccinationDTO>(n));
            }    

            return vaccinationDTOs;
        }

        public Task<int> UpdateVaccinationAsync(VaccinationDTO dto)
        {

            unitOfWork.Vaccination.Update(mapper.Map<Vaccination>(dto));
            return unitOfWork.Commit();
        }

        public async Task<List<VaccinationTypeDTO>> AddvaccinationTypesAsync()
        {

            return await unitOfWork.VaccinationType.Get().Select(type => mapper.Map<VaccinationTypeDTO>(type)).ToListAsync();
              
        }


        public async Task<int> GetUserIdByEmailAdressAsync(string email)
        {
            var idaccount = await accountService.GetIdAccountByEmailAsync(email);

            return userService.GetUsersByAccountId(idaccount).Result.Id;
 
        }

        public async Task<int> RemoveVaccinationAsync(int id)
        {
           var entity = await unitOfWork.Vaccination.GetByIdAsync(id);

            if (entity != null)
            {
                unitOfWork.Vaccination.Remove(entity);
            }
            return await unitOfWork.Commit();
        }

    }

}
