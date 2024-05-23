using System.Globalization;
using System.Windows.Data;

namespace LibraryApp
{
    public class NameToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string IdSpec = values[0].ToString();
            string IdValue = values[1].ToString();
            if (IdSpec != null && IdValue != null)
            {
                return "Check" + IdSpec + "_" + IdValue;
            }
            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
