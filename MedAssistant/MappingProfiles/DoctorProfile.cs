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
                .ForMember(dto => dto.DoctorType, opt => opt.MapFrom(prescription => prescription.DoctorType.Type))
                .ForMember(dto => dto.MedicalInstitutionName, opt => opt.MapFrom(prescription => prescription.MedicalInstitution.Name));
            CreateMap<DoctorDTO, Doctor>();


            CreateMap<DoctorDTO, DoctorModel>();
            CreateMap<DoctorModel, DoctorDTO>();

            //CreateMap<DoctorDTO, CreateDoctorModel>()
            //    .ForMember(model => model.DoctorTypes, opt => opt.MapFrom(dto => dto.DoctorType))
            //    .ForMember(model => model.MedicalInstitutionName, opt => opt.MapFrom(dto => dto.MedicalInstitutionDTO.));
            //CreateMap<CreateDoctorModel, DoctorDTO>().ForMember(dto => dto., opt => opt.MapFrom(model => model.Medicine));

        } 
    }
}
