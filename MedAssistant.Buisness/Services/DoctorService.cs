using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;
using Microsoft.EntityFrameworkCore;


namespace MedAssistant.Buisness.Services
{
    public class DoctorService : IDoctorService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public DoctorService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService,
            IAccountService accountService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userService = userService;
            this.accountService = accountService;
        }

        

        public async Task<List<DoctorDTO>> GetAllDoctorsByEmailAsync(string email)
        {
            try
            {

                var accountid = await accountService.GetIdAccountByEmailAsync(email);
                var user = await userService.GetUsersByAccountId(accountid);
                var doctors = await unitOfWork.Doctor.FindBy(doc => doc.UserId.Equals(user.Id), x => x.MedicalInstitution, x => x.DoctorType).ToListAsync();

               
                return doctors.Select(x => mapper.Map<DoctorDTO>(x)).ToList();
               
            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<DoctorDTO> GetDoctorByIdAsync(int id)
        {
            try
            {
                var entity = await unitOfWork.Doctor.FindBy(doc => doc.Id.Equals(id), x => x.MedicalInstitution, x => x.DoctorType).FirstOrDefaultAsync();
                return mapper.Map<DoctorDTO>(entity);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<List<DoctorTypeDTO>> GetAllDoctorTypesAsync()
        {
            try
            {
                var DoctorTypes = await unitOfWork.DoctorType.Get().Select(x=>mapper.Map<DoctorTypeDTO>(x)).ToListAsync();
                return DoctorTypes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MedicalInstitutionDTO>> GetAllMedicalInstitutionsAsync()
        {
            try
            {
                var medicalInstitutionDTOs = await unitOfWork.MedicalInstitution.Get().Select(x => mapper.Map<MedicalInstitutionDTO>(x)).ToListAsync();
                return medicalInstitutionDTOs;
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

        public Task<int> AddDoctorAsync(DoctorDTO dto)
        {
            try
            {
                unitOfWork.Doctor.AddAsync(mapper.Map<Doctor>(dto));
                return unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> RemoveDoctorAsync(DoctorDTO dto)
        {
            try
            {
                unitOfWork.Doctor.Remove(mapper.Map<Doctor>(dto));
                return unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> UpdateDoctorAsync(DoctorDTO dto)
        {
            try
            {
                unitOfWork.Doctor.Update(mapper.Map<Doctor>(dto));
                return unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
