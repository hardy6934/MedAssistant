using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class Note : IBaseEntity
    {
        public int Id { get; set; }
        public bool Remind { get; set; }
        public string? Description { get; set; }
        public DateTime? RemindDate { get; set; }
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }  

        public int NoteTypeId { get; set; }
        public NoteType NoteType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
