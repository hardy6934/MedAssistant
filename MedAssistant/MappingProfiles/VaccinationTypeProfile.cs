using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class VaccinationTypeProfile: Profile
    { 
        public VaccinationTypeProfile()
        {

            CreateMap<VaccinationType, VaccinationTypeDTO>();
            CreateMap<VaccinationTypeDTO, VaccinationType>();


            CreateMap<VaccinationTypeDTO, VaccinationTypeModel>();
            CreateMap<VaccinationTypeModel, VaccinationTypeDTO>();

        }
         
    }
}
