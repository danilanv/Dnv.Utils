using System;
using System.Windows.Data;
using System.Globalization;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Преобразует число размер файла в human readable form.
    /// </summary>
    [ValueConversion(typeof(long), typeof(string))]
    public class FileSizeConverter: IValueConverter
    {
        private const double KbSize = 1024;
        private const double MbSize = 1024 * 1024;
        private const double GbSize = 1024 * 1024 * 1024;
        private const int DigitsNumber = 1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = (double)(long)value;
            
            // размер меньше килобайта, отображаем байты
            if (size < KbSize)
                return Math.Round(size, DigitsNumber).ToString(CultureInfo.InvariantCulture) + " б";
            // размер меньше мегабайта, отображаем килобайты
            if (size >= KbSize && size <= MbSize)
                return Math.Round(size / KbSize, DigitsNumber).ToString(CultureInfo.InvariantCulture) + " Кб";
            // размер меньше гигабайта, отображаем мегабайты
            if (size <= GbSize)
                return Math.Round(size / MbSize, DigitsNumber).ToString(CultureInfo.InvariantCulture) + " Мб";

            return Math.Round(size / GbSize, DigitsNumber).ToString(CultureInfo.InvariantCulture) + " ГБ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
