namespace MedAssistant.WebAPI.Models.Responses
{
    public class VaccinationsResponseModel
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public bool Remind { get; set; }
        public DateTime? RemindDate { get; set; }
        public int UserId { get; set; } 
        public int VaccinationTypeId { get; set; }
        
    }
}
