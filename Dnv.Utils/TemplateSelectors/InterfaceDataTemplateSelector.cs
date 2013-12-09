using System.Windows;
using System.Windows.Controls;

namespace Dnv.Utils.TemplateSelectors
{
    /// <summary>
    /// Alows to select DataTemplate for View Model which implement's given interface.
    /// <code>
    /// <Window.Resources>
    /// <InterfaceDataTemplateSelector x:Key="selector" InterfaceName="IItem" DataTemplateName="locomotiveViewModelDataTemplate"/>
    /// 
    /// <DataTemplate x:Key="dataTemplate" DataType="{x:Type IItem}">
    ///     <TextBox Text="{Binding Name}"/>
    /// </DataTemplate>
    /// 
    /// </Window.Resources>
    /// 
    /// <TreeView ItemsSource="{Binding Path=ItemsList}"
    ///           ItemTemplateSelector="{StaticResource selector}">
    /// </TreeView> 
    /// </code>
    /// 
    /// </summary>
    public class InterfaceDataTemplateSelector: DataTemplateSelector
    {
        /// <summary>
        /// Interface name. 
        /// </summary>
        public string InterfaceName { get; set; }

        /// <summary>
        /// Name of DataTemplate.
        /// </summary>
        public string DataTemplateName { get; set; }

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item != null && item.GetType().GetInterface(InterfaceName) != null)
            {
                var frameworkElement = container as FrameworkElement;
                if (frameworkElement != null)
                    return frameworkElement.FindResource(DataTemplateName) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
