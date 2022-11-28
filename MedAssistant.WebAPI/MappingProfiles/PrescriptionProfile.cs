using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;

namespace MedAssistant.MappingProfiles
{
    public class PrescriptionProfile : Profile
    {
        public PrescriptionProfile()
        {

            //CreateMap<Prescription, PrescriptionDTO>()
            //    .ForMember(dto => dto.MedicineName, opt => opt.MapFrom(prescription => prescription.Medicine.Name))
            //    .ForMember(dto =>dto.MedicineType, opt =>opt.MapFrom(prescription =>prescription.Medicine.MedicineType.Type));
            //CreateMap<PrescriptionDTO, Prescription>();


        } 
    }
}
