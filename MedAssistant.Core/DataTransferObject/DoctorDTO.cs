using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string SName { get; set; }
        public string? LName { get; set; }
        public string? PhoneNumber { get; set; }
        public int DoctorTypeId { get; set; }
        public DoctorTypeDTO DoctorTypeDTO { get; set; }

        public int MedicalInstitutionId { get; set; }
        public MedicalInstitutionDTO MedicalInstitutionDTO { get;set;}

     }
}
