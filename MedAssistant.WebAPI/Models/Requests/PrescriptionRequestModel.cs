namespace MedAssistant.WebAPI.Models.Requests
{
    public class PrescriptionRequestModel
    {
        public string? Dosage { get; set; }
        public bool Bought { get; set; }

        public int MedicineId { get; set; }
          
        public int UserId { get; set; }
    }
}
