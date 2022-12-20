using AutoMapper;
using MedAssistant.Core.Abstractions;
using MedAssistant.Models.Calendar;
using Microsoft.AspNetCore.Mvc;

namespace MedAssistant.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEventsForCalendarService eventsForCalendarService;

        public CalendarController(IMapper mapper, IEventsForCalendarService eventsForCalendarService) { 
        this.mapper = mapper;
            this.eventsForCalendarService = eventsForCalendarService;
        }

        public async Task<IActionResult> Index()
        {
            var login = HttpContext.User.Identity.Name;
            var events = await eventsForCalendarService.GetEventsForCurrentUserAsync(login);
            var model =  new CalendarModelBuilder().Build(events.Select(x=>mapper.Map<EventModel>(x)).ToList());

            return View("CalendarView", model);
        }
    }
}
