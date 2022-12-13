using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedAssistant.Models
{
    public class CreatePrescriptionModel
    {
        public int Id { get; set; }
        public string? Dosage { get; set; }
        public bool Bought { get; set; }

        public string MedicineId { get; set; }
        public List<SelectListItem> Medicine { get; set; }

        public string? MedicineName { get;set; }

        public int UserId { get; set; }

    }
}
