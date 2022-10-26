using MedAssistant.DataBase.Entities;
using AutoMapper;
using MedAssistant.Models;
using MedAssistant.Core.DataTransferObject;

namespace MedAssistant.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            
            CreateMap<User, UserDTO >();
            CreateMap<UserDTO, User>();


            CreateMap<UserDTO, UserModel>();
            CreateMap<UserModel, UserDTO>();

            CreateMap<UserDTO, UserShortDataModel>();

        }
    }
}
