using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedAssistant.Models
{
    public class CreateDoctorModel
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }

        public int DoctorTypeId { get; set; }        
        public List<SelectListItem> DoctorTypes { get; set; }

        public int MedicalInstitutionId { get; set; }
        public List<SelectListItem> MedicalInstitutionName { get; set; }


 
    }
}
