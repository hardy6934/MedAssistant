using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public bool Remind { get; set; }
        public DateTime? RemindDate { get; set; }
        public string? Description { get; set; }

        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorPhone { get; set; }
        public string? MedicalInstitution { get; set; }

        public int? NoteTypeId { get; set; }
        public string? NoteType { get; set; }

        public int UserId { get; set; }
    }
}
