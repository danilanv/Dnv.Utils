using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Dnv.Utils.Converters
{
    [ValueConversion(typeof(SolidColorBrush), typeof(Color))]
    public class SolidColorBrushToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = (SolidColorBrush)value;
            return brush.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            return new SolidColorBrush(color);
        }
    }
}
