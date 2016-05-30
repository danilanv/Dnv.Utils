using System;
using System.Windows;

namespace Dnv.Utils.Messages
{

    /// <summary>
    /// Сообщение для отображения MessageBox
    /// </summary>
    public class MessageBoxMessage
    {
        private readonly string _text;
        private readonly string _caption;
        private readonly MessageBoxButton _button;
        private readonly MessageBoxImage _icon;
        private readonly Action<MessageBoxResult> _resultCallback;

        public MessageBoxMessage(string text, string caption, MessageBoxButton button, MessageBoxImage icon, Action<MessageBoxResult> resultCallback)
        {
            _text = text;
            _caption = caption;
            _button = button;
            _icon = icon;
            _resultCallback = resultCallback;
        }

        public void Show(Window owner)
        {
            Result = MessageBox.Show(owner, _text, _caption, _button, _icon);
        }


        public MessageBoxResult Result { get; private set; }


        /// <summary>
        /// Обработать результат. Вызывается принимающей стороной.
        /// </summary>
        public void ProcessResult()
        {
            _resultCallback(Result);
        }
    }
}