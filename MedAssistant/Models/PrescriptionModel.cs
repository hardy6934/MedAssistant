using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class PrescriptionModel
    {
        public int Id { get; set; }
        
        public string? Dosage { get; set; }
        [Required]
        public bool Bought { get; set; }

        [Required]
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }

        public string? MedicineType { get; set; }

        public int UserId { get; set; }

    }
}
