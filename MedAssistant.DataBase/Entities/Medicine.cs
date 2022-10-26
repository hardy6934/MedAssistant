using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class Medicine : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Picture { get; set; }
        public string? Description { get; set; }

        public int MedicineTypeId { get; set; }
        public MedicineType MedicineType { get; set; }

        public List<Prescription> Prescriptions { get; set; }

    }
}
