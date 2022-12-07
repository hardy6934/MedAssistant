using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MedAssistant.Core.Abstractions
{
    public interface IDoctorService
    {
        Task<int> GetUserIdByEmailAdressAsync(string email);
        Task<List<DoctorDTO>> GetAllDoctorsByEmailAsync(string email);
        Task<DoctorDTO> GetDoctorByIdAsync(int id);
        Task<List<MedicalInstitutionDTO>> GetAllMedicalInstitutionsAsync();
        Task<List<DoctorTypeDTO>> GetAllDoctorTypesAsync();
        Task<int> AddDoctorAsync(DoctorDTO dto);
        Task<int> UpdateDoctorAsync(DoctorDTO dto);
        Task<int> RemoveDoctorAsync(DoctorDTO dto);

    }
}
