using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Dnv.Utils.Behaviors
{
    public class PasswordBehavior : Behavior<PasswordBox>
    {
        public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(PasswordBehavior), new PropertyMetadata(default(string)));

        private object _value;

        private bool _skipUpdate;

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PasswordChanged += PasswordBox_PasswordChanged;
            if (_value != null)
            {
                AssociatedObject.Password = _value as string;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= PasswordBox_PasswordChanged;
            base.OnDetaching();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            // in my case this called before OnAttached() 
            base.OnPropertyChanged(e);
            if (AssociatedObject == null)
            {
                // so, let'save the value and then reuse it when OnAttached() called
                _value = e.NewValue as string;
                return;
            }

            if (e.Property == PasswordProperty)
            {
                if (!_skipUpdate)
                {
                    _skipUpdate = true;
                    AssociatedObject.Password = e.NewValue as string;
                    _skipUpdate = false;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _skipUpdate = true;
            Password = AssociatedObject.Password;
            _skipUpdate = false;
        }
    }
}
