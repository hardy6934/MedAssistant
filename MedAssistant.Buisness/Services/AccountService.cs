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
            try
            {
                dto.Password = CreateMd5(dto.Password);
                await unitOfWork.Accounts.AddAsync(mapper.Map<Account>(dto));

                return await unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task EditAccountAsync(AccountDTO dto)
        {
            try
            {
                unitOfWork.Accounts.Update(mapper.Map<Account>(dto));
                await unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task<bool> IsAccountExistAsync(AccountDTO dto)
        {
            try
            {
                dto.Password = CreateMd5(dto.Password);
                return await unitOfWork.Accounts.Get().AnyAsync(x => x.Equals(mapper.Map<Account>(dto)));
            }
            catch (Exception)
            { 
                throw;
            } 
        }


        public async Task<AccountDTO> GetAccountByIdAsync(int id)
        {
            try
            {
                var account = await unitOfWork.Accounts.GetByIdAsync(id); 
                return mapper.Map<AccountDTO>(account);
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task<int> GetIdAccountByEmailAsync(string email)
        {
            try
            {
                var accountId = (await unitOfWork.Accounts.Get().FirstOrDefaultAsync(x => x.Login.Equals(email))).Id; 
                return accountId;
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                var account = unitOfWork.Accounts.Get().Where(x => x.Login.Equals(email)).FirstOrDefault();

                if (account == null)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public async Task RemoveAccountAsync(AccountDTO dto)
        {
            try
            {
                unitOfWork.Accounts.Remove(mapper.Map<Account>(dto));
                await unitOfWork.Commit();
            }
            catch (Exception)
            { 
                throw;
            } 
        }


        public async Task<bool> CheckUserPassword(AccountDTO dto)
        {
            try
            {
                var dbPasswordHash = (await unitOfWork.Accounts.Get().AsNoTracking().FirstOrDefaultAsync(x => x.Login.Equals(dto.Login)))?.Password;

                if (dbPasswordHash != null && CreateMd5(dto.Password).Equals(dbPasswordHash))
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            { 
                throw;
            } 
        }
         

        private string CreateMd5(string password)
        {
            try
            {
                var passwordSalt = "qwe";

                using MD5 md5 = MD5.Create();
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
                var hashBytes = md5.ComputeHash(inputBytes); 
                return Convert.ToHexString(hashBytes);
            }
            catch (Exception)
            {

                throw;
            } 
        } 

    }
}
