using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.WebAPI.MappingProfiles
{
    public class DoctorTypeProfile : Profile
    {
        public DoctorTypeProfile() {

            CreateMap<DoctorTypeRequestModel, DoctorTypeDTO>();
            CreateMap<DoctorTypeDTO, DoctorTypeRequestModel>();
             
             
            CreateMap<DoctorTypeDTO, ResponseDoctorTypeModel>();
        }
    }
}
