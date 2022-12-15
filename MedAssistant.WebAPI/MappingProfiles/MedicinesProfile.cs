using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.MappingProfiles
{
    public class MedicinesProfile : Profile
    {

        public MedicinesProfile() {

            CreateMap<MedicineDTO, MedicinesRequestModel>();
            CreateMap<MedicinesRequestModel, MedicineDTO>();

            CreateMap<MedecinesResponseModel, MedicineDTO>();
            CreateMap<MedicineDTO, MedecinesResponseModel>();
        }

    }
}
