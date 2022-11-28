using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.MappingProfiles
{
    public class MedicalInstitutionProfile : Profile
    {
        public MedicalInstitutionProfile() {

            CreateMap<MedicalInstitution, MedicalInstitutionDTO>();
            CreateMap<MedicalInstitutionDTO, MedicalInstitution>();


        }
    }
}
