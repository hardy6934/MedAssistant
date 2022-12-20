using MedAssistant.Core;
using MedAssistant.Core.Abstractions;

namespace MedAssistant.Models.Calendar
{
    public class DayModelBuilder 
    { 
        public DayModel Build(DateTime date, List<EventModel> events)
        {

            return new DayModel()
            {
                Date = date,
                IsNotCurrentMonth = date.Month != DateTime.Now.Month,
                IsWeekendOrHoliday = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday,
                IsToday = date.Date == DateTime.Now.Date,

                Events = events

            };
        }
    }
}
