using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class VaccinationDTO
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public bool Remind { get; set; }
        public DateTime? RemindDate { get; set; }
        public int UserId { get; set; }

        public int VaccinationTypeId { get; set; }      
        public string VaccinationType { get; set; }
        public List<SelectListItem> Types { get; set; }




    }
}
