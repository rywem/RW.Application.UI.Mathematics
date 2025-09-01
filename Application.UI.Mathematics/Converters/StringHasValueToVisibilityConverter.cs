using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RW.Application.UI.Mathematics.Converters
{
    public class StringHasValueToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public static readonly StringHasValueToVisibilityConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool show = value is string s && !string.IsNullOrWhiteSpace(s);
            return show ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}
