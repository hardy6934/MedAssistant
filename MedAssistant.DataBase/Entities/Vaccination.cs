using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class Vaccination : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public bool Remind { get; set; }
        public DateTime? RemindDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int VaccinationTypeId { get; set; }
        public VaccinationType VaccinationType { get; set; }

    }
}
