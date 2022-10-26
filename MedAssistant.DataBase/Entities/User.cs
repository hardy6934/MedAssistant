using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class User : IBaseEntity
    {
        public int Id {get;set;}
        public string FullName {get;set;}
         
        public DateTime? Birthday { get; set; }
        public string? Location { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public List<Prescription> Prescriptions { get; set; }
        public List<Note> Notes { get; set; }
        public List<Vaccination> Vaccinations { get; set; }
    }
}
