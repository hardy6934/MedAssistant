using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IPrescriptionService
    { 
        Task<List<PrescriptionDTO>> GetPrescriptionsbyUserEmailAsync(string email);
        Task<List<MedicineDTO>> GetAllMedicinesAsync();
        Task<int> GetUserIdByEmailAdressAsync(string email);
        Task<int> CreatePrescriptionAsync(PrescriptionDTO prescriptionDTO);
        Task<int> UpdatePrescriptionAsync(PrescriptionDTO dto);
        Task<PrescriptionDTO> GetPrescriptionByIdAsync(int id);
        Task<int> RemovePrescriptionAsync(int id); 
    }
}
