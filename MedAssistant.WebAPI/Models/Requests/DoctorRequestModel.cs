namespace MedAssistant.WebAPI.Models.Requests
{
    public class DoctorRequestModel
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DoctorTypeId { get; set; }
        public int MedicalInstitutionId { get; set; }
        public int UserId { get; set; } 
    }
}
