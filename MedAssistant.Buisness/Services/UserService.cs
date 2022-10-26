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
            var user = new UserDTO { FullName = "User", Location = "", Birthday = null, AccountId = IdAccount, RoleId = IdRole };
             
            return user;
        }

        public async Task<int> CreateUserAsync(UserDTO dto)
        {   

            await _unitOfWork.Users.AddAsync(_mapper.Map<User>(dto));
            return await _unitOfWork.Commit();

        }

        public async Task<UserDTO> GetUsersByAccountId(int Accountid)
        {

            var user = await _unitOfWork.Users.FindBy(us => us.AccountId.Equals(Accountid), user => user.Account, us =>us.Role).FirstOrDefaultAsync();

          
            return _mapper.Map<UserDTO>(user);

        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {

            return _mapper.Map<UserDTO>(await _unitOfWork.Users.GetByIdAsync(id));
                 
        }

        public async Task<int> UpdateUserAsync(UserDTO dto)
        {
           
            _unitOfWork.Users.Update(_mapper.Map<User>(dto));

            return await _unitOfWork.Commit();
        }


        
    }
}
