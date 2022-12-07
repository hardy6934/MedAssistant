using MedAssistant.Core.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface INoteTypeService
    {
        Task<List<NoteTypeDTO>> GetAllNoteTypes();
        Task<NoteTypeDTO> GetNoteTypeByIdAsync(int id);
        Task<int> AddNoteTypeAsync(NoteTypeDTO dto);
        Task<int> UpdateNoteTypeAsync(NoteTypeDTO dto);
        Task<int> RemoveNoteTypeAsync(NoteTypeDTO dto);
    }
}
