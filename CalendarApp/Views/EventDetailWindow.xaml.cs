using System.Windows;
using CalendarApp.Models;

namespace CalendarApp.Views
{
    public partial class EventDetailWindow : Window
    {
        private readonly CalendarEvent _event;

        public EventDetailWindow(CalendarEvent ev)
        {
            InitializeComponent();
            _event = ev;
            TitleTextBlock.Text = _event.Title;
            DateTimeTextBlock.Text = _event.DateTime.ToString("g");
            DescriptionTextBlock.Text = _event.Description;
            CompletedCheckBox.IsChecked = _event.IsCompleted;
        }

        private void CompletedCheckBox_Click(object sender, RoutedEventArgs e)
        {
            _event.IsCompleted = CompletedCheckBox.IsChecked ?? false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}