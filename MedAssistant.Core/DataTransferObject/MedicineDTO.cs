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
        public byte[]? Picture { get; set; }
        public string? Description { get; set; }

        public int MedicineTypeId { get; set; }
        public MedicineTypeDTO MedicineTypeDTO { get; set; }

        public List<PrescriptionDTO> PrescriptionsDTO { get; set; }

    }
}
