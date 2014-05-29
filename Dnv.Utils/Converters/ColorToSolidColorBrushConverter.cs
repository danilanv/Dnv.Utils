using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Dnv.Utils.Converters
{
    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var brush = (SolidColorBrush)value;
            return brush.Color;
        }
    }
}
