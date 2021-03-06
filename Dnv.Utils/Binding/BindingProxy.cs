﻿using System.Windows;

namespace Dnv.Utils.Binding
{
    /// <summary>
    /// Author of this code is Thomas Levesque. (http://www.thomaslevesque.com/2011/03/21/wpf-how-to-bind-to-data-when-the-datacontext-is-not-inherited/)
    /// Placed here because i use this code in my projects.
    /// Прокси объект, который позволяет биндится когда DataContext не унаследован. Использование:
    /// <BindingProxy x:Key="_bindingProxy" Data="{Binding Path=ViewModel, ElementName=_this}"/>
    /// ...
    /// <object SomeProperty="{Binding Data.ChartCloseCommand, Source={StaticResource _bindingProxy}}"/>
    /// </summary>
    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}
