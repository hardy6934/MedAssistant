using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class MedicineTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        
        public List<MedicineDTO> MedicinesDTO { get; set; }

    }
}
