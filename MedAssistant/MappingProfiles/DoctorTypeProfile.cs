using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{ 
        public class DoctorTypeProfile : Profile
        {
            public DoctorTypeProfile()
            {

                CreateMap<DoctorType, DoctorTypeDTO>();
                CreateMap<DoctorTypeDTO, DoctorType>();


                CreateMap<DoctorTypeDTO, DoctorTypeModel>();
                CreateMap<DoctorTypeModel, DoctorTypeDTO>();

            }

        } 
}
