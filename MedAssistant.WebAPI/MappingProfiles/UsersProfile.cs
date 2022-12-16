using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;

namespace MedAssistant.WebAPI.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {

            CreateMap<UserDTO, UserResponseModel>();
            CreateMap<UserResponseModel, UserDTO>();

            CreateMap<UserDTO, UserResponseModel>();
            CreateMap<UserResponseModel, UserDTO>();

        }
    }
}
