using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class MedicalInstitutionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<DoctorDTO> DoctorsDTO { get; set; }
    }
}
