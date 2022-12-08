using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;



namespace MedAssistant.Buisness.Services
{
    public class VaccinationTypeService : IVaccinationTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public VaccinationTypeService(IUnitOfWork unitOfWork, IMapper mapper) { 

            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> AddVaccinationTypeAsync(VaccinationTypeDTO dto)
        {
            try
            {
                await unitOfWork.VaccinationType.AddAsync(mapper.Map<VaccinationType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<List<VaccinationTypeDTO>> GetAllVaccinationTypes()
        {
            try
            {
                var Dtos = await unitOfWork.VaccinationType.GetAllAsync();
                return Dtos.Select(x => mapper.Map<VaccinationTypeDTO>(x)).ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VaccinationTypeDTO> GetVaccinationTypeByIdAsync(int id)
        {
            try
            {
                var entity = mapper.Map<VaccinationTypeDTO>(await unitOfWork.VaccinationType.GetByIdAsync(id));

                return entity;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<int> RemoveVaccinationTypeAsync(VaccinationTypeDTO dto)
        { 
            try
            {
                unitOfWork.VaccinationType.Remove(mapper.Map<VaccinationType>(dto));
                var result = await unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        public async Task<int> UpdateVaccinationTypeAsync(VaccinationTypeDTO dto)
        {
            try
            {
                unitOfWork.VaccinationType.Update(mapper.Map<VaccinationType>(dto));
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
