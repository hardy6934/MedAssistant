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

        Task<List<DoctorDTO>> GetAllDoctorsByEmailAsync(string email);

    }
}
