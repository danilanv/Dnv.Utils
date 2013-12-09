using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Converts System.Collections.ICollection to System.Windows.Visibility.
    /// If ICollection.Count > 0, return Visibility.Visible else Visibility.Collapsed.
    /// </summary>
    public class CollectionItemsCountToVisibilityConverter: IValueConverter
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
                throw new ArgumentException("CollectionItemsCountToVisibilityConverter: argument value must be of ICollection type.");

            if (VisibleIfGtZero)
                return collection.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
           
            return collection.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
