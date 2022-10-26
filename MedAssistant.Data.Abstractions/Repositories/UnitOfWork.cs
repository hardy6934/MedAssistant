using MedAssistant.Data.Repositories;
using MedAssistant.Data.Repositories.Repositories;
using MedAssistant.DataBase;
using MedAssistant.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Data.Abstractions.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly MedAssistantContext _context;
        public IRepository<User> Users { get; }
        public IRepository<Account> Accounts { get; }
        public IRepository<Role> Role { get; }
        public IRepository<Vaccination> Vaccination { get; }
        public IRepository<VaccinationType> VaccinationType { get; }
        public IRepository<Prescription> Prescription { get; }
        public IRepository<Medicine> Medicines { get; }

        public UnitOfWork(MedAssistantContext context,
            IRepository<User> users,
            IRepository<Account> accounts,
            IRepository<Role> role,
            IRepository<Vaccination> vaccination,
            IRepository<VaccinationType> vaccinationType,
            IRepository<Prescription> prescription,
            IRepository<Medicine> medicines)
        {
            _context = context;
            Users = users;
            Accounts = accounts;
            Role = role;
            Vaccination = vaccination;
            VaccinationType = vaccinationType;
            Prescription = prescription;
            Medicines = medicines;
        }



        public async Task<int> Commit()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
