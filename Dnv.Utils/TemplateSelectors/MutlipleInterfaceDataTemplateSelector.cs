using System.Collections.Generic;
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

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemType = item.GetType();

            foreach (var dataTemplateItem in Items)
            {
                if (itemType.GetInterface(dataTemplateItem.InterfaceName) != null)
                {
                    var frameworkElement = container as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        var kj = frameworkElement.FindResource(dataTemplateItem.DataTemplateName) as DataTemplate;
                        return frameworkElement.FindResource(dataTemplateItem.DataTemplateName) as DataTemplate;
                    }
                }
            }

            return base.SelectTemplate(item, container);
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
