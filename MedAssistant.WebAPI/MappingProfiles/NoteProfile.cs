using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.WebAPI.MappingProfiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {

            CreateMap<NoteRequestModel, NoteDTO>();
            CreateMap<NoteDTO, NoteRequestModel>();

            CreateMap<NoteResponseModel, NoteDTO>();
            CreateMap<NoteDTO, NoteResponseModel>();

        }
    }
}
