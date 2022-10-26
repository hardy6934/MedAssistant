﻿using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile() 
        {
        
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();


            CreateMap<AccountDTO, AuthenticationModel>();
            CreateMap<AuthenticationModel, AccountDTO>();

        }
    }
}
