using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.WebAPI.Models.Requests;
using MedAssistant.WebAPI.Models.Responses;

namespace MedAssistant.MappingProfiles
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {

            CreateMap<PrescriptionRequestModel, PrescriptionDTO>();
            CreateMap<PrescriptionDTO, PrescriptionRequestModel>();

            CreateMap<PrescriptionsResponseModel, PrescriptionDTO>();
            CreateMap<PrescriptionDTO, PrescriptionsResponseModel>();

        } 
    }
}
