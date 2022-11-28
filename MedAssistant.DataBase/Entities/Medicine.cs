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
        public string? MedecineUrl { get; set; }

        public List<Prescription> Prescriptions { get; set; }

    }
}
