using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
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
            catch (Exception e)
            {
                throw;
            }


        }


    }
}
