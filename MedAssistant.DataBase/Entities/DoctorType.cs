using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class DoctorType : IBaseEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        
        public List<Doctor> Doctors { get; set; }
    }
}
