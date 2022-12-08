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
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public NoteService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService, IUserService userService) { 
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.accountService = accountService;   
            this.userService = userService;
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


        public async Task<List<DoctorDTO>> GetAllDoctorsForUserByEmailAsync(string email)
        {
            try
            {
                var userId = await GetUserIdByEmailAdressAsync(email);
                var doctors = await unitOfWork.Doctor.FindBy(doc => doc.UserId.Equals(userId), x => x.MedicalInstitution, x => x.DoctorType).ToListAsync();
                
                return doctors.Select(x => mapper.Map<DoctorDTO>(x)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
}

        public async Task<List<NoteTypeDTO>> GetAllNoteTypesAsync()
        {
            try
            {
                return await unitOfWork.NoteType.Get().Select(x => mapper.Map<NoteTypeDTO>(x)).ToListAsync();
            }
            catch (Exception)
            {
                throw; 
            }
        }

        public async Task<NoteDTO> GetNoteByIdAsync(int id)
        {
            try
            {
                var dto = await unitOfWork.Note.FindBy(x => x.Id.Equals(id), x => x.Doctor, x => x.User, x => x.NoteType, x => x.Doctor.MedicalInstitution).FirstOrDefaultAsync();
                return mapper.Map<NoteDTO>(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NoteDTO>> GetNotesbyUserEmailAsync(string email)
        {
            try
            {
                var userId =  await GetUserIdByEmailAdressAsync(email);
                var doctors = await unitOfWork.Note.FindBy(doc => doc.UserId.Equals(userId), x => x.Doctor, x => x.User, x=>x.NoteType, x=>x.Doctor.MedicalInstitution).ToListAsync();

                return doctors.Select(x => mapper.Map<NoteDTO>(x)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<int> CreateNoteAsync(NoteDTO noteDTO)
        {
            try
            {
                await unitOfWork.Note.AddAsync(mapper.Map<Note>(noteDTO));
                return await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<int> RemoveNoteAsync(int id)
        {
            try
            {
                var entity = await unitOfWork.Note.GetByIdAsync(id);

                if (entity != null)
                {
                    unitOfWork.Note.Remove(entity);
                }
                return await unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<int> UpdateNoteAsync(NoteDTO noteDTO)
        {
            try
            {
                unitOfWork.Note.Update(mapper.Map<Note>(noteDTO));
                return unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
