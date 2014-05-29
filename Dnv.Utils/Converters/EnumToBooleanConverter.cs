using System;
using System.Globalization;
using System.Windows.Data;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Конвертер перечислений в bool. Пример использования:
    /// <StackPanel>
    /// <StackPanel.Resources>          
    ///     <local:EnumToBooleanConverter x:Key="EnumToBooleanConverter" />          
    /// </StackPanel.Resources>
    ///     <RadioButton IsChecked="{Binding Path=YourEnumProperty, Converter={StaticResource EnumToBooleanConverter}, ConverterParameter={x:Static local:YourEnumType.Enum1}}" />
    ///     <RadioButton IsChecked="{Binding Path=YourEnumProperty, Converter={StaticResource EnumToBooleanConverter}, ConverterParameter={x:Static local:YourEnumType.Enum2}}" />
    /// </StackPanel>
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : System.Windows.Data.Binding.DoNothing;
        }
    }
}
