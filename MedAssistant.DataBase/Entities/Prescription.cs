using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class Prescription : IBaseEntity
    {
        public int Id { get; set; }
        public string? Dosage { get; set; }
        public bool Bought { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
