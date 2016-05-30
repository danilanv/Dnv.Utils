using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Преобразует true в false и наоборот. За счет расширения MarkupExtension можно использовать без создания отдельного экземпляра в ресурсах.
    /// 
    /// <example>
    /// <code>
    /// IsHitTestVisible="{Binding Path=IsOpen, Converter={namespace:InverseBooleanConverter}, ElementName=modifiersPopup}"
    /// </code>
    /// </example>
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : MarkupExtension, IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
