using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class MedicalInstitutionProfile : Profile
    {
        public MedicalInstitutionProfile() {

            CreateMap<MedicalInstitution, MedicalInstitutionDTO>();
            CreateMap<MedicalInstitutionDTO, MedicalInstitution>();

            CreateMap<MedicalInstitutionDTO, MedicalInstitutionModel>();
            CreateMap<MedicalInstitutionModel, MedicalInstitutionDTO>();

        }
    }
}
