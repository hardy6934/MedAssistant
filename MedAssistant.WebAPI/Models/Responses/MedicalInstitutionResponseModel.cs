namespace MedAssistant.WebAPI.Models.Responses
{
    public class MedicalInstitutionResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? InfoUrl { get; set; }
    }
}
