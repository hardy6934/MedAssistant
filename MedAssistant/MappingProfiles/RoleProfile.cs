using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() {

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

        }


    }
}
