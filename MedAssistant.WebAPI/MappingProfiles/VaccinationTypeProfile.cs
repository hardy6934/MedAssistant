using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.MappingProfiles
{
    public class VaccinationTypeProfile: Profile
    { 
        public VaccinationTypeProfile()
        {

            CreateMap<VaccinationTypeRequestModel, VaccinationTypeDTO>();
            CreateMap<VaccinationTypeDTO, VaccinationTypeRequestModel>();

            CreateMap<VaccinationTypeResponseModel, VaccinationTypeDTO>();
            CreateMap<VaccinationTypeDTO, VaccinationTypeResponseModel>();
        }
         
    }
}
