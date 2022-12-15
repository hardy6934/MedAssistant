namespace MedAssistant.WebAPI.Models.Requests
{
    public class VaccinationRequestModel
    {
         public DateTime date { get; set; }
        public bool Remind { get; set; }
        public DateTime? RemindDate { get; set; }
        public int UserId { get; set; }

        public int VaccinationTypeId { get; set; }
         
    }
}
