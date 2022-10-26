using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAssistant.Data.Repositories.Repositories;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Account> Accounts { get; }
        IRepository<Role> Role { get; }
        IRepository<Vaccination> Vaccination { get; }
        IRepository<Prescription> Prescription { get; }
        IRepository<VaccinationType> VaccinationType { get; }
        IRepository<Medicine> Medicines { get; }
       

        Task<int> Commit();
    }
}
