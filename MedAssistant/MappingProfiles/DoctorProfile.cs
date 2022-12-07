using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class DoctorProfile : Profile
    { 
        public DoctorProfile() {

            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dto => dto.DoctorType, opt => opt.MapFrom(doctor => doctor.DoctorType.Type))
                .ForMember(dto => dto.MedicalInstitutionName, opt => opt.MapFrom(doctor => doctor.MedicalInstitution.Name));
            CreateMap<DoctorDTO, Doctor>();


            CreateMap<DoctorDTO, DoctorModel>();
            CreateMap<DoctorModel, DoctorDTO>();

            CreateMap<DoctorDTO, CreateDoctorModel>();
            CreateMap<CreateDoctorModel, DoctorDTO>();

        } 
    }
}
