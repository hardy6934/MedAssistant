using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.Abstractions
{
    public interface IEventsForCalendarService
    {
        Task<List<Event>> GetEventsForCurrentUserAsync(string login);
    }
}
