using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IMedicalInstitutionService
    { 
        Task AddMedicalInstitutionsAsync(); 


        Task<List<MedicalInstitutionDTO>> GetAllMedicalInstitutionsFromDataBaseAsync();
        Task<MedicalInstitutionDTO> GetMedicalInstitutionByIdAsync(int id);
        Task<int> AddMedicalInstitutionAsync(MedicalInstitutionDTO dto);
        Task<int> UpdateMedicalInstitutionAsync(MedicalInstitutionDTO dto);
        Task<int> RemoveMedicalInstitutionAsync(MedicalInstitutionDTO dto);
    }
}
