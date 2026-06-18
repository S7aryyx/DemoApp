using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CalendarApp.Models;
using CalendarApp.Services;

namespace CalendarApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly IDataService _dataService = new LocalDataService();
        private List<Month> _months;
        private List<CalendarEvent> _events;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadMonthsAsync();
        }

        private async Task LoadMonthsAsync()
        {
            ShowLoading(true);
            try
            {
                _months = new List<Month>(await _dataService.GetMonthsAsync());
                MonthsListBox.ItemsSource = _months;
                if (_months.Count > 0)
                    MonthsListBox.SelectedIndex = 0;
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private async void MonthsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthsListBox.SelectedItem is Month selectedMonth)
            {
                await LoadEventsAsync(selectedMonth.Id);
            }
        }

        private async Task LoadEventsAsync(int monthId)
        {
            ShowLoading(true);
            try
            {
                _events = new List<CalendarEvent>(await _dataService.GetEventsAsync(monthId));
                EventsListBox.ItemsSource = _events;
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void ShowLoading(bool show)
        {
            LoadingProgress.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            LoadingText.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }

        private void EventsListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (EventsListBox.SelectedItem is CalendarEvent selectedEvent)
            {
                OpenDetails(selectedEvent);
            }
        }

        private void OpenDetails(CalendarEvent ev)
        {
            var detailWindow = new EventDetailWindow(ev)
            {
                Owner = this
            };
            detailWindow.ShowDialog();
        }
    }
}