using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class NoteTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public List<NoteDTO> NotesDTO { get; set; }

    }
}
