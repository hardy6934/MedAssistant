using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public string? Dosage { get; set; }
        public bool Bought { get; set; }

        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string MedicineType { get; set; }
        public List<SelectListItem> Names { get; set; }

        public int UserId { get; set; }
    }
}
