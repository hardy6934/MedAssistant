
using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IUserService
    {
         
        Task<UserDTO> GetUserByIdAsync(int id);

        Task<int> CreateUserAsync(UserDTO dto);
        UserDTO CreateDefaultUserUserAsync(int IdAccount, int IdRole);
        Task<UserDTO> GetUsersByAccountId(int AccountId); 
        Task<List<UserDTO>> GetAllUsers();
        Task<int> UpdateUserAsync(UserDTO dto);
        Task<List<RoleDTO>> GetAllRolesAsync();
        Task<int> ChangeUserRoleByEmail(string email, string newRole);

    }
}
