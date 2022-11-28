using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.MappingProfiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {

            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();


        }
    }
}
