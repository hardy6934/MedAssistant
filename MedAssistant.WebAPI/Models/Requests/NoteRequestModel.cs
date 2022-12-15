namespace MedAssistant.WebAPI.Models.Requests
{
    public class NoteRequestModel
    {
        public bool Remind { get; set; }
        public DateTime? RemindDate { get; set; }
        public string? Description { get; set; }

        public int? DoctorId { get; set; }

        public int? NoteTypeId { get; set; }

        public int UserId { get; set; }
    }
}
