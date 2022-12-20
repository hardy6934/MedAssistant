using AutoMapper;
using MedAssistant.Core;
using MedAssistant.Core.Abstractions;
using MedAssistant.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Buisness.Services
{
    public class EventsForCalendarService : IEventsForCalendarService
    { 
        private readonly IMapper mapper; 
        private readonly IVaccinationService vaccinationService;
        private readonly INoteService noteService;

        public EventsForCalendarService(IMapper mapper, IVaccinationService vaccinationService, INoteService noteService) {
            this.mapper = mapper; 
            this.vaccinationService = vaccinationService;
            this.noteService = noteService;
        }

        public async Task<List<Event>> GetEventsForCurrentUserAsync(string login)
        { 
            List<Event> list = new();

            var vaccinationEvents = await vaccinationService.GetVaccinationsbyUserEmailAsync(login);
            var noteEvents = await noteService.GetNotesbyUserEmailAsync(login);

            list.AddRange(vaccinationEvents.Select(x => mapper.Map<Event>(x)).ToList());
            list.AddRange(noteEvents.Select(x => mapper.Map<Event>(x)).ToList());

            return list;
        }
    }
}
