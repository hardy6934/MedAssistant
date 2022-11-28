using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;

namespace MedAssistant.WebAPI.MappingProfiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile() 
        {
        
            CreateMap<AccountDTO, RegAccountRequestModel>().ForMember(reg => reg.Email, opt => opt.MapFrom(dto => dto.Login));
            CreateMap<RegAccountRequestModel, AccountDTO>().ForMember(dto => dto.Login, opt => opt.MapFrom(reg => reg.Email));
            

            CreateMap<AccountDTO, LoginAccountRequestModel>().ForMember(reg => reg.Email, opt => opt.MapFrom(dto => dto.Login));
            CreateMap<LoginAccountRequestModel, AccountDTO>().ForMember(dto => dto.Login, opt => opt.MapFrom(reg => reg.Email));


        }
    }
}
