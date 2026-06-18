using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CalendarApp.Models;

namespace CalendarApp.Services
{
    public class LocalDataService : IDataService
    {
        private readonly string _filePath = "data.json";
        private List<Month> _months;
        private List<CalendarEvent> _events;
        private bool _isLoaded = false;

        private async Task LoadDataAsync()
        {
            if (_isLoaded) return;
            var json = await File.ReadAllTextAsync(_filePath);
            var data = JsonSerializer.Deserialize<DataRoot>(json);
            _months = data.Months;
            _events = data.Events;
            _isLoaded = true;
        }

        public async Task<IEnumerable<Month>> GetMonthsAsync()
        {
            await LoadDataAsync();
            return _months;
        }

        public async Task<IEnumerable<CalendarEvent>> GetEventsAsync(int monthId)
        {
            await LoadDataAsync();
            return _events.Where(e => e.MonthId == monthId);
        }

        private class DataRoot
        {
            public List<Month> Months { get; set; }
            public List<CalendarEvent> Events { get; set; }
        }
    }
}