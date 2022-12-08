using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Buisness.Services
{
    public class DoctorTypeService : IDoctorTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DoctorTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> AddDoctorTypeAsync(DoctorTypeDTO dto)
        {
            try
            {
                await unitOfWork.DoctorType.AddAsync(mapper.Map<DoctorType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<List<DoctorTypeDTO>> GetAllDoctorTypes()
        {
            try
            {
                var Dtos = await unitOfWork.DoctorType.GetAllAsync();
                return Dtos.Select(x => mapper.Map<DoctorTypeDTO>(x)).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DoctorTypeDTO> GetDoctorTypeByIdAsync(int id)
        {
            try
            {
                var entity = mapper.Map<DoctorTypeDTO>(await unitOfWork.DoctorType.GetByIdAsync(id));

                return entity;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<int> RemoveDoctorTypeAsync(DoctorTypeDTO dto)
        {
            try
            {
                unitOfWork.DoctorType.Remove(mapper.Map<DoctorType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<int> UpdateDoctorTypeAsync(DoctorTypeDTO dto)
        {
            try
            {
                unitOfWork.DoctorType.Update(mapper.Map<DoctorType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }
    }
}
