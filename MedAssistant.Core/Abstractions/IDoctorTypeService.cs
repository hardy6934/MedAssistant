using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IDoctorTypeService
    {
        Task<List<DoctorTypeDTO>> GetAllDoctorTypes();
        Task<DoctorTypeDTO> GetDoctorTypeByIdAsync(int id);
        Task<int> AddDoctorTypeAsync(DoctorTypeDTO dto);
        Task<int> UpdateDoctorTypeAsync(DoctorTypeDTO dto);
        Task<int> RemoveDoctorTypeAsync(DoctorTypeDTO dto);
        Task GetAllDoctorTypesFromMedTutorial();
    }
}
