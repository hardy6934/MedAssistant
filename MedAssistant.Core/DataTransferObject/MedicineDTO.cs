using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? MedecineUrl { get; set; }


        public List<PrescriptionDTO> PrescriptionsDTO { get; set; }

    }
}
