using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Converts System.Collections.ICollection to System.Windows.Visibility.
    /// If ICollection.Count > 0, return Visibility.Visible else Visibility.Collapsed.
    /// Unfortunately does not works when items count changes.
    /// </summary>
    public class CollectionItemsCountToVisibilityConverter: MarkupExtension, IValueConverter
    {
        public bool VisibleIfGtZero { get; set; }

        public CollectionItemsCountToVisibilityConverter()
        {
            VisibleIfGtZero = true;
        }

        #region Implementation of IValueConverter

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = value as ICollection;
            if (collection == null)
                return Visibility.Collapsed;

            if (VisibleIfGtZero)
                return collection.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
           
            return collection.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
