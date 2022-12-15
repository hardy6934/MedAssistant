using MedAssistant.DataBase.Entities;
using AutoMapper;
using MedAssistant.Models;
using MedAssistant.Core.DataTransferObject;

namespace MedAssistant.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            
            CreateMap<User, UserDTO >().ForMember(dto => dto.AccountLogin, opt => opt.MapFrom(acc => acc.Account.Login))
          .ForMember(dto => dto.RoleName, opt => opt.MapFrom(acc => acc.Role.Name)); 
            CreateMap<UserDTO, User>();


            CreateMap<UserDTO, UserModel>();
            CreateMap<UserModel, UserDTO>();

            CreateMap<UserDTO, UserShortDataModel>();

            CreateMap<UserDTO, EditUsersRoleModel>();
            CreateMap<EditUsersRoleModel, UserDTO>();

        }
    }
}
