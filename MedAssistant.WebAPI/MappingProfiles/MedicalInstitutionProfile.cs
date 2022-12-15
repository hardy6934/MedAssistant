using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.MappingProfiles
{
    public class MedicalInstitutionProfile : Profile
    {
        public MedicalInstitutionProfile() {

            CreateMap<MedicalInstitutionRequestModel, MedicalInstitutionDTO>();
            CreateMap<MedicalInstitutionDTO, MedicalInstitutionRequestModel>();

            CreateMap<MedicalInstitutionResponseModel, MedicalInstitutionDTO>();
            CreateMap<MedicalInstitutionDTO, MedicalInstitutionResponseModel>();

        }
    }
}
