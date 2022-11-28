using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.MappingProfiles
{
    public class VaccinationTypeProfile: Profile
    { 
        public VaccinationTypeProfile()
        {

            CreateMap<VaccinationType, VaccinationTypeDTO>();
            CreateMap<VaccinationTypeDTO, VaccinationType>();

             
        }
         
    }
}
