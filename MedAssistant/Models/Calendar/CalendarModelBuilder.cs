using MedAssistant.Core.Abstractions;

namespace MedAssistant.Models.Calendar
{
    public class CalendarModelBuilder 
    {
        
        public CalendarModel Build(List<EventModel> events)
        {
            DayModel[,] days = new DayModel[6, 7];
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int offset = (int)date.DayOfWeek;

            if (offset == 0)
                offset = 7;

            offset--;
            date = date.AddDays(offset * -1);

            for (int i = 0; i != 6; i++)
            {
                for (int j = 0; j != 7; j++)
                {
                    days[i, j] =   new DayModelBuilder().Build(date, events);
                    date = date.AddDays(1);
                }
            }
            return new CalendarModel()
            {
                Date = DateTime.Now,
                Days = days
            };
        }
    }
}
