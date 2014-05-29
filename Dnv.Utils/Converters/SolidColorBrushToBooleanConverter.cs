using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Преобразователь SolidColorBrush в Bool.
    /// </summary>
    [ValueConversion(typeof(SolidColorBrush), typeof(bool))]
    public class SolidColorBrushToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush)value == (SolidColorBrush)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
