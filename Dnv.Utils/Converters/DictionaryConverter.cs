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
    /// <para/>
    /// <example>
    /// <code>
    /// class ViewModel{ public Dictionary&lt;string, DateTime&gt; Dict {get;} } 
    /// <para/>
    /// &lt;DictionaryConverter x:Key="dictionaryConverter"/&gt;
    /// &lt;object&gt;
    ///     &lt;object.Property&gt;  
    ///         &lt;MultiBinding Converter="{StaticResource dictionaryConverter}"
    ///                       ConverterParameter="Millisecond"
    ///                       UpdateSourceTrigger="PropertyChanged"&gt;
    ///             &lt;Binding Path="{Binding Path=Dict}"/&gt;
    ///             &lt;Binding Path="Key"/&gt;
    ///         &lt;/MultiBinding&gt;  
    ///     &lt;/object.Property&gt;    
    /// &lt;/object&gt;
    /// </code>
    /// </example>
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
