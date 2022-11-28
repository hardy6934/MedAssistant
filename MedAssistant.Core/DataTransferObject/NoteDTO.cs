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
        public int DoctorId { get; set; }
        public DoctorDTO? DoctorDTO { get; set; }  

        public int NoteTypeId { get; set; }
        public NoteTypeDTO NoteTypeDTO { get; set; }

        public int UserId { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
