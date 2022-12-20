using AutoMapper;
using MedAssistant.Core;
using MedAssistant.Models.Calendar;

namespace MedAssistant.MappingProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {

            CreateMap<Event, EventModel>();
            CreateMap<EventModel, Event>();

             

        }

    }
}
