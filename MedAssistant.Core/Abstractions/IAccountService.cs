using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IAccountService
    { 
          
        Task<int> CreateAccountAsync(AccountDTO dto);

        bool IsEmailExist(string email);

        Task EditAccountAsync(AccountDTO dto);

        Task RemoveAccountAsync(AccountDTO dto);

        Task<bool> IsAccountExistAsync(AccountDTO dto);

        Task<bool> CheckUserPassword(AccountDTO dto);

        Task<AccountDTO> GetAccountByIdAsync(int id);

        Task<int> GetIdAccountByEmailAsync(string email);
    }
}
