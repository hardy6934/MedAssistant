using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.MappingProfiles
{
    public class DoctorProfile : Profile
    { 
        public DoctorProfile() {

            CreateMap<DoctorRequestModel, DoctorDTO>();
            CreateMap<DoctorDTO, DoctorRequestModel>();

            CreateMap<DoctorResponseModel, DoctorDTO>();
            CreateMap<DoctorDTO, DoctorResponseModel>();

        } 
    }
}
