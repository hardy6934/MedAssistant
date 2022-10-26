using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IVaccinationService
    { 
        Task<VaccinationDTO> GetVaccinationByIdAsync(int id);

        Task<int> CreateVaccinationAsync(VaccinationDTO dto);

        Task<List<VaccinationDTO>> GetVaccinationsbyUserEmailAsync(string email);
        Task<int> UpdateVaccinationAsync(VaccinationDTO dto);
        Task<List<VaccinationTypeDTO>> AddvaccinationTypesAsync();

        Task<int> GetUserIdByEmailAdressAsync(string email);

        Task<int> RemoveVaccinationAsync(int id);
    }
}
