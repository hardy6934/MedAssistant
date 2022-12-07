using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IMedicineService
    {
         Task GetAllMedecinesFromTabletkaAsync();
         Task<List<MedicineDTO>> GetAllMedecinesFromDataBaseAsync();
         Task<MedicineDTO> GetMedecineByIdAsync(int id);
         Task<int> AddMedecineAsync(MedicineDTO dto);
         Task<int> UpdateMedecineAsync(MedicineDTO dto);
         Task<int> RemoveMedecineAsync(MedicineDTO dto);
    }
}
