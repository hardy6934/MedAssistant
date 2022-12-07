using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface INoteService
    {
        Task<List<NoteDTO>> GetNotesbyUserEmailAsync(string email);
        Task<List<DoctorDTO>> GetAllDoctorsForUserByEmailAsync(string email);
        Task<List<NoteTypeDTO>> GetAllNoteTypesAsync();
        Task<int> GetUserIdByEmailAdressAsync(string email);
        Task<int> CreateNoteAsync(NoteDTO noteDTO);
        Task<int> UpdateNoteAsync(NoteDTO noteDTO);
        Task<NoteDTO> GetNoteByIdAsync(int id);
        Task<int> RemoveNoteAsync(int id);
    }
}
