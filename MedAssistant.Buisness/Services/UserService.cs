using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MedAssistant.Buisness.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;        
       

        public UserService(IUnitOfWork unitOfWork, IMapper mapper )
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
             

        }

        public UserDTO CreateDefaultUserUserAsync(int IdAccount, int IdRole)
        {
            try
            {
                var user = new UserDTO { FullName = "User", Location = "", Birthday = null, AccountId = IdAccount, RoleId = IdRole };
                return user;
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task<int> CreateUserAsync(UserDTO dto)
        {
            try
            {
                await _unitOfWork.Users.AddAsync(_mapper.Map<User>(dto));
                return await _unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task<UserDTO> GetUsersByAccountId(int Accountid)
        {
            try
            {
                var user = await _unitOfWork.Users.FindBy(us => us.AccountId.Equals(Accountid), user => user.Account, us => us.Role).FirstOrDefaultAsync(); 
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.Get().Include(x=>x.Account).Include(x=>x.Role).ToListAsync(); 
                return users.Select(x => _mapper.Map<UserDTO>(x)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> ChangeUserRoleByEmail(string email, string newRole)
        {
            try
            {
                var accountId = _unitOfWork.Accounts.FindBy(ac => ac.Login.Equals(email)).FirstOrDefault().Id;
                var user = await _unitOfWork.Users.FindBy(us => us.AccountId.Equals(accountId)).FirstOrDefaultAsync();

                var role = await _unitOfWork.Role.FindBy(x=>x.Name.Equals(newRole)).AsNoTracking().FirstOrDefaultAsync();
                if (role != null && user != null)
                {
                    user.RoleId = role.Id;
                    _unitOfWork.Users.Update(user);
                    return await _unitOfWork.Commit();
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<UserDTO>(await _unitOfWork.Users.GetByIdAsync(id));
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task<int> UpdateUserAsync(UserDTO dto)
        {
            try
            {
                _unitOfWork.Users.Update(_mapper.Map<User>(dto)); 
                return await _unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            }
            
        }

        public async Task<List<RoleDTO>> GetAllRolesAsync()
        { 
            var dtos = await _unitOfWork.Role.GetAllAsync(); 

            return dtos.Select(x=> _mapper.Map<RoleDTO>(x)).ToList();
        }


        
    }
}
