using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class MedicalInstitution : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? InfoUrl { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
