using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class NoteTypeProfile : Profile
    {
        public NoteTypeProfile()
        {

            CreateMap<NoteType, NoteTypeDTO>();
            CreateMap<NoteTypeDTO, NoteType>();


            CreateMap<NoteTypeDTO, NoteTypeModel>();
            CreateMap<NoteTypeModel, NoteTypeDTO>();

        }
    }
}
