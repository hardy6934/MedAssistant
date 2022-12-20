using AutoMapper;
using MedAssistant.Core;
using MedAssistant.Core.DataTransferObject;
using MedAssistant.DataBase.Entities;
using MedAssistant.Models;

namespace MedAssistant.MappingProfiles
{
    public class NoteProfile : Profile
    {

        public NoteProfile() {

            CreateMap<Note, NoteDTO>()
                .ForMember(dto => dto.DoctorName, opt => opt.MapFrom(note => note.Doctor.FullName))
                .ForMember(dto => dto.DoctorPhone, opt => opt.MapFrom(note => note.Doctor.PhoneNumber))
                .ForMember(dto => dto.NoteType, opt => opt.MapFrom(note => note.NoteType.Type))
                .ForMember(dto => dto.MedicalInstitution, opt => opt.MapFrom(note => note.Doctor.MedicalInstitution.Name));
            CreateMap<NoteDTO, Note>();


            CreateMap<NoteDTO, NoteModel>();
            CreateMap<NoteModel, NoteDTO>();

            CreateMap<NoteDTO, CreateNoteModel>();
            CreateMap<CreateNoteModel, NoteDTO>();

            CreateMap<NoteDTO, Event>().ForMember(x => x.Date, x => x.MapFrom(note => note.RemindDate))
                .ForMember(x => x.Name, x => x.MapFrom(note => note.NoteType));

        }

    }
}
