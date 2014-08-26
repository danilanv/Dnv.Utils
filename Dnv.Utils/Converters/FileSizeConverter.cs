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
        private const int KbSize = 1024;
        private const int MbSize = 1024*1024;
        private const int GbSize = 1024 * 1024 * 1024;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = (long)value;
            
            // размер меньше килобайта, отображаем байты
            if (size < KbSize)
                return size.ToString(CultureInfo.InvariantCulture) + " б";
            // размер меньше мегабайта, отображаем килобайты
            if (size >= KbSize && size <= MbSize)
                return (size / KbSize).ToString(CultureInfo.InvariantCulture) + " Кб";
            // размер меньше гигабайта, отображаем мегабайты
            if (size <= GbSize)
                return (size / (MbSize)).ToString(CultureInfo.InvariantCulture) + " Мб";

            return (size /GbSize).ToString(CultureInfo.InvariantCulture) + " ГБ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
