using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.MappingProfiles
{
    public class DoctorProfile : Profile
    { 
        public DoctorProfile() {

            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dto => dto.DoctorType, opt => opt.MapFrom(prescription => prescription.DoctorType.Type))
                .ForMember(dto => dto.MedicalInstitutionName, opt => opt.MapFrom(prescription => prescription.MedicalInstitution.Name));
            CreateMap<DoctorDTO, Doctor>();

        } 
    }
}
