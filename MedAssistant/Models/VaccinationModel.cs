using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using System.ComponentModel.DataAnnotations;

namespace MedAssistant.Models
{
    public class VaccinationModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }

        [Required]
        public bool Remind { get; set; }


        public DateTime? RemindDate { get; set; }
        public int UserId { get; set; }

        [Required]
        public int VaccinationTypeId { get; set; }

        public string? VaccinationType { get; set; }

         

    }
}
