using MedAssistant.Core.Abstractions;
using MedAssistant.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Buisness.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IUnitOfWork unitOfWork) { 
        
            this.unitOfWork = unitOfWork;

        }

        public async Task<int> FindRoleIdByRoleName(string RoleName)
        {
            try
            {
                var id = (await unitOfWork.Role.Get().AsNoTracking().FirstOrDefaultAsync(x => x.Name == RoleName)).Id; 
                return id;
            }
            catch (Exception)
            { 
                throw;
            }
            
        }
    }
}
