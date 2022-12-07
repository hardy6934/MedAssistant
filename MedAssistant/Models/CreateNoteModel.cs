using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedAssistant.Models
{
    public class CreateNoteModel
    {
        public int Id { get; set; }
        public bool Remind { get; set; }
        public string? Description { get; set; }
        public DateTime? RemindDate { get; set; }

        public int? DoctorId { get; set; }
        public List<SelectListItem> ListOfDoctors { get; set; }

        public int NoteTypeId { get; set; }
        public List<SelectListItem> ListOfNoteTypes { get; set; }
         
    }
}
