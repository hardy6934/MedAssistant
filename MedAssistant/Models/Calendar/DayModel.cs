namespace MedAssistant.Models.Calendar
{
    public class DayModel
    {
        public DateTime Date { get; set; }
        public bool IsNotCurrentMonth { get; set; }
        public bool IsWeekendOrHoliday { get; set; }
        public bool IsToday { get; set; }
        public List<EventModel> Events { get; set; }
    }
}
