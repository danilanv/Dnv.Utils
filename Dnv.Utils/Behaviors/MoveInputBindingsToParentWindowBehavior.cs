using System;
using System.Windows;

namespace Dnv.Utils.Behaviors
{
    /// <summary>
    /// Moves FrameworkElement's InputBindings to the parent window. May be used to move UserControl keybord shortcarts to the main window.
    /// </summary>
    public class MoveInputBindingsToParentWindowBehavior
    {
        public static readonly DependencyProperty MoveProperty = DependencyProperty.RegisterAttached("Mode",
            typeof (bool),
            typeof(MoveInputBindingsToParentWindowBehavior),
            new FrameworkPropertyMetadata(false, OnChanged));

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement) d;
            element.Loaded += ElementOnLoaded;
        }

        private static void ElementOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var element = (FrameworkElement) sender;
            var parent = Window.GetWindow(element);
            if (parent == null)
                return;

            for (int i = element.InputBindings.Count - 1; i >= 0; i--)
            {
                var inpuitBinding = element.InputBindings[i];
                parent.InputBindings.Add(inpuitBinding);
                element.InputBindings.Remove(inpuitBinding);
            }
        }

        public static void SetMove(UIElement element, bool val)
        {
            element.SetValue(MoveProperty, val);
        }

        public static bool GetMove(UIElement element)
        {
            return (bool) element.GetValue(MoveProperty);
        }
    }
}