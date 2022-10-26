using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedAssistant.Models
{
    public class CreateVaccinationModel
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public bool Remind { get; set; }
        public DateTime? RemindDate { get; set; }
        public int UserId { get; set; }

        public int VaccinationTypeId { get; set; }
        public List<SelectListItem> Types { get; set; }
    }
}
