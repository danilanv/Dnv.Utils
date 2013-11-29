using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Позволяет получать объекты из словаря при использовании с MultiBinding. 
    /// Объявления биндингов в MultiBinding должны идти в порядке: словарь, ключ.
    /// ConverterParameter может указывать на название свойства объекта, которое требуется возвратить из полученного значения Value .
    /// Пример:
    /// class ViewModel{ public Dictionary<string, DateTime> Dict {get;} }
    /// 
    /// <DictionaryConverter x:Key="dictionaryConverter"/>
    /// <object>
    ///     <object.Property>  
    ///         <MultiBinding Converter="{StaticResource dictionaryConverter}"
    ///                       ConverterParameter="Millisecond"
    ///                       UpdateSourceTrigger="PropertyChanged">
    ///             <Binding Path="{Binding Path=Dict}"/>
    ///             <Binding Path="Key"/>
    ///         </MultiBinding>  
    ///     </object.Property>    
    /// </object>
    /// </summary>
    public class DictionaryConverter : IMultiValueConverter 
    {
        #region Implementation of IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) 
                throw new Exception("Parameters count must be 2!");

            var dict = values[0] as IDictionary;
            var key = values[1];
            
            if (dict == null || key == null)
                return null;

            if (parameter == null)
                return dict[key];

            var name = parameter as string;
            if (name == null)
                throw new Exception("DictionaryConverter: parameter should be the name of the property");

            var obj = dict[key];
            if (obj == null)
                return null;

            var p = obj.GetType().GetProperty(name);
            return p.GetValue(obj, null);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
