using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {

            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();


            CreateMap<AccountDTO, RegistrationModel>();
            CreateMap<RegistrationModel, AccountDTO>();

        }
    }
}
