using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Dnv.Utils.TemplateSelectors
{
    /// <summary>
    /// <example>
    /// <code>
    /// interface IBar
    /// {}
    /// 
    /// interface IFoo: IBase
    /// {}
    /// 
    /// </code> 
    /// <code>
    /// <Window.Resources>
    ///     <MutlipleInterfaceDataTemplateSelector x:Key="selector">
    ///         <MutlipleInterfaceDataTemplateSelector.Items>
    ///             <DataTemplateItem InterfaceName="IFoo" DataTemplateName="fooDataTemplate"/>
    ///             <DataTemplateItem InterfaceName="IBar" DataTemplateName="barDataTemplate"/>
    ///         </MutlipleInterfaceDataTemplateSelector.Items>
    ///     </MutlipleInterfaceDataTemplateSelector>
    /// 
    ///     <DataTemplate x:Key="fooDataTemplate" DataType="{x:Type IFoo}">
    ///         <TextBlock Text="this is foo!"/>
    ///     </DataTemplate>
    /// 
    ///     <DataTemplate x:Key="barDataTemplate" DataType="{x:Type IBar}">
    ///         <TextBlock Text="this is bar!"/>
    ///     </DataTemplate>
    /// </Window.Resources>
    /// 
    /// <TreeView ItemsSource="{Binding Path=Items}"
    ///           ItemTemplateSelector="{StaticResource selector}"/>
    /// </code>
    /// </example>
    /// </summary>
    public class MutlipleInterfaceDataTemplateSelector: DataTemplateSelector
    {
        private MutlipleInterfaceDataTemplateSelector _basedOn;

        public MutlipleInterfaceDataTemplateSelector()
        {
            Items = new List<DataTemplateItem>();
        }

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemType = item.GetType();

            foreach (var dataTemplateItem in Items)
            {
                if (itemType.GetInterface(dataTemplateItem.InterfaceName) != null)
                {
                    var frameworkElement = container as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        var template =
                            frameworkElement.FindResource(dataTemplateItem.DataTemplateName) as DataTemplate;
                        if (template == null)
                            throw new ArgumentNullException(
                                "MutlipleInterfaceDataTemplateSelector: can't find template " +
                                dataTemplateItem.DataTemplateName);

                        return template;
                    }
                }
            }

            return null;
        }

        public List<DataTemplateItem> Items { get; set; }


        public MutlipleInterfaceDataTemplateSelector BasedOn
        {
            get { return _basedOn; }
            set
            {
                if (_basedOn == value)
                    return;

                _basedOn = value;

                Items.InsertRange(0, _basedOn.Items);
            }
        }
    }
}
