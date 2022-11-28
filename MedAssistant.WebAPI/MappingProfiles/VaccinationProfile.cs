using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.WebAPI.MappingProfiles
{
    public class VaccinationProfile : Profile
    {
        public VaccinationProfile()
        {
             
            CreateMap<VaccinationDTO, VaccinationsResponseModel>();
            CreateMap<VaccinationsResponseModel, VaccinationDTO>();

        }




    }
     
}
