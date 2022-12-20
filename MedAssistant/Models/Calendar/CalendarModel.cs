namespace MedAssistant.Models.Calendar
{
    public class CalendarModel
    {
        public DateTime Date { get; set; }
        public DayModel[,] Days { get; set; }

    }
}
