using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.WebAPI.MappingProfiles
{
    public class VaccinationProfile : Profile
    {
        public VaccinationProfile()
        {

            CreateMap<VaccinationDTO, VaccinationRequestModel>();
            CreateMap<VaccinationRequestModel, VaccinationDTO>();

            CreateMap<VaccinationDTO, VaccinationsResponseModel>();
            CreateMap<VaccinationsResponseModel, VaccinationDTO>();

        }




    }
     
}
