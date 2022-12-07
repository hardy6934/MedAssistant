using AutoMapper;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class MedicinesProfile : Profile
    {

        public MedicinesProfile() {

            CreateMap<Medicine, MedicineDTO>();
            CreateMap<MedicineDTO, Medicine>();


            CreateMap<MedicineDTO, MedicineModel>();
            CreateMap<MedicineModel, MedicineDTO>();

        }

    }
}
