using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.WebAPI.MappingProfiles
{
    public class NoteTypeProfile : Profile
    {
        public NoteTypeProfile() { 

            CreateMap<NoteTypeRequestModel, NoteTypeDTO>();
            CreateMap<NoteTypeDTO, NoteTypeRequestModel>();

            CreateMap<NoteTypeResponseModel, NoteTypeDTO>();
            CreateMap<NoteTypeDTO, NoteTypeResponseModel>();

        }
    }
}
