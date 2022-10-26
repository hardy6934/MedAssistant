using MedAssistant.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase
{
    public class MedAssistantContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorType> DoctorTypes { get; set; }
        public DbSet<MedicalInstitution> MedicalInstitutions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineType> MedicineTypes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteType> NoteTypes { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<VaccinationType> VaccinationTypes { get; set; }
        public DbSet<Role> Roles { get; set; }


        public MedAssistantContext(DbContextOptions<MedAssistantContext> options)
            : base(options)
        { 
         
        }

    }
}
