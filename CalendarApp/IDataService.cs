using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarApp.Models;

namespace CalendarApp.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Month>> GetMonthsAsync();
        Task<IEnumerable<CalendarEvent>> GetEventsAsync(int monthId);
    }
}