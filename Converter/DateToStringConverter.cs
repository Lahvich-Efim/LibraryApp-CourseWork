using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LibraryApp
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                ObservableCollection<string> monthName = Application.Current.Resources["m_Month"] as ObservableCollection<string>;
                string formattedDate = $"{dateTime.Day} {monthName[dateTime.Month - 1]} {dateTime.Year}, {dateTime.ToString("HH:mm")}";
                return formattedDate;
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
