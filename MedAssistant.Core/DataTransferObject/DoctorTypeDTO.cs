using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class DoctorTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        
        public List<DoctorDTO> DoctorsDTO { get; set; }
    }
}
