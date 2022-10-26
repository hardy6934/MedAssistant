using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {

            CreateMap<Prescription, PrescriptionDTO>()
                .ForMember(dto => dto.MedicineName, opt => opt.MapFrom(prescription => prescription.Medicine.Name))
                .ForMember(dto =>dto.MedicineType, opt =>opt.MapFrom(prescription =>prescription.Medicine.MedicineType.Type));
            CreateMap<PrescriptionDTO, Prescription>();


            CreateMap<PrescriptionDTO, PrescriptionModel>();
            CreateMap<PrescriptionModel, PrescriptionDTO>();

            CreateMap<PrescriptionDTO, CreatePrescriptionModel>().ForMember(model => model.Medicine, opt => opt.MapFrom(dto => dto.Names));
            CreateMap<CreatePrescriptionModel, PrescriptionDTO>().ForMember(dto => dto.Names, opt => opt.MapFrom(model => model.Medicine)); ;

        } 
    }
}
