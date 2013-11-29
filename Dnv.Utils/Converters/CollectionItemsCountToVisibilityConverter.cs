using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Dnv.Utils.Converters
{
    /// <summary>
    /// Если число элементов коллекции больше 0, то возвращает Visibility.Visible иначае Visibility.Collapsed
    /// </summary>
    class CollectionItemsCountToVisibilityConverter: IValueConverter
    {
        public bool VisibleIfGtZero { get; set; }

        public CollectionItemsCountToVisibilityConverter()
        {
            VisibleIfGtZero = true;
        }

        #region Implementation of IValueConverter

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
