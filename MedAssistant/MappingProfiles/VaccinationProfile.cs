using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;
 

namespace MedAssistant.MappingProfiles
{
    public class VaccinationProfile : Profile
    {
        public VaccinationProfile()
        {

            CreateMap<Vaccination, VaccinationDTO>().ForMember(x => x.VaccinationType, x => x.MapFrom(vaccination => vaccination.VaccinationType.Type));
            CreateMap<VaccinationDTO, Vaccination>();


            CreateMap<VaccinationDTO, VaccinationModel>();
            CreateMap<VaccinationModel, VaccinationDTO>();

            CreateMap<VaccinationDTO, CreateVaccinationModel>();
            CreateMap<CreateVaccinationModel, VaccinationDTO>();

        }




    }
     
}
