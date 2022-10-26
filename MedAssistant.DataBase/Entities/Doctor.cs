using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class Doctor : IBaseEntity
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string? LName { get; set; }
        public string? PhoneNumber { get; set; }
        public int DoctorTypeId { get; set; }
        public DoctorType DoctorType { get; set; }

        public int MedicalInstitutionId { get; set; }
        public MedicalInstitution MedicalInstitution { get;set;}

        public List<Note> Notes { get; set; }

    }
}
