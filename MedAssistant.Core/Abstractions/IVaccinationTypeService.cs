using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IVaccinationTypeService
    {
        Task<List<VaccinationTypeDTO>> GetAllVaccinationTypes();
        Task<VaccinationTypeDTO> GetVaccinationTypeByIdAsync(int id);
        Task<int> AddVaccinationTypeAsync(VaccinationTypeDTO dto);
        Task<int> UpdateVaccinationTypeAsync(VaccinationTypeDTO dto);
        Task<int> RemoveVaccinationTypeAsync(VaccinationTypeDTO dto);
    }
}
