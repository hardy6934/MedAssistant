using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.Data.Repositories;
using MedAssistant.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


namespace MedAssistant.Buisness.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
         


        public AccountService(IUnitOfWork unitOfWork, IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
             
        }

        public async Task<int> CreateAccountAsync(AccountDTO dto)
        {
            dto.Password = CreateMd5(dto.Password);
            await unitOfWork.Accounts.AddAsync(mapper.Map<Account>(dto));

            return await unitOfWork.Commit();
        }

        public async Task EditAccountAsync(AccountDTO dto)
        {
            unitOfWork.Accounts.Update(mapper.Map<Account>(dto));
            await unitOfWork.Commit();
        }

        public async Task<bool> IsAccountExistAsync(AccountDTO dto)
        {
            dto.Password = CreateMd5(dto.Password);
            return await unitOfWork.Accounts.Get().AnyAsync(x => x.Equals(mapper.Map<Account>(dto)));
             
        }


        public async Task<AccountDTO> GetAccountByIdAsync(int id)
        { 
            var account = await unitOfWork.Accounts.GetByIdAsync(id);
             
                return mapper.Map<AccountDTO>(account);
             
        }

        public async Task<int> GetIdAccountByEmailAsync(string email)
        {
            var accountId =  (await unitOfWork.Accounts.Get().FirstOrDefaultAsync(x => x.Login.Equals(email))).Id;

            return accountId;

        }

        public bool IsEmailExist(string email)
        {

            var account = unitOfWork.Accounts.Get().Where(x => x.Login.Equals(email)).FirstOrDefault();

            if (account == null)
            {
                return false;
            }
            else
                return true;
        }

        public async Task RemoveAccountAsync(AccountDTO dto)
        {
            unitOfWork.Accounts.Remove(mapper.Map<Account>(dto));
            await unitOfWork.Commit();
        }


        public async Task<bool> CheckUserPassword(AccountDTO dto)
        {
            var dbPasswordHash = (await unitOfWork.Accounts.Get().AsNoTracking().FirstOrDefaultAsync(x => x.Login.Equals(dto.Login)))?.Password;

            if (dbPasswordHash != null && CreateMd5(dto.Password).Equals(dbPasswordHash))
            {
                return true;
            }
            else
                return false;

        }

         


        private string CreateMd5(string password)
        {
            var passwordSalt =  "qwe";

            using MD5 md5 = MD5.Create();
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
            var hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }



    }
}
