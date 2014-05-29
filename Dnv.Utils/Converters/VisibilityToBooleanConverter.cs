using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Преобразователь Bool в Visibility.
    /// </summary>
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class VisibilityToBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
