namespace MedAssistant.WebAPI.Models.Responses
{
    public class PrescriptionsResponseModel
    {
        public int Id { get; set; }
        public string? Dosage { get; set; }
        public bool Bought { get; set; }

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineType { get; set; }

        public int UserId { get; set; }
    }
}
