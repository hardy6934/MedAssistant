using MedAssistant.Core.DataTransferObject;

namespace MedAssistant.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }

        public int DoctorTypeId { get; set; }
        public string? DoctorType { get; set; }
         
        public int MedicalInstitutionId { get; set; }
        public string? MedicalInstitutionName { get; set; }

        public int UserId { get; set; }
    }
}
