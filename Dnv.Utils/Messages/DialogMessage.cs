using System;
using System.Windows.Forms;

namespace Dnv.Utils.Messages
{
    /// <summary>
    /// Сообщение, используется для отображения диалога. Вызывающая должна создать и сконфигурировать диалог.
    /// Принимающая сторона должна отобразить диалог и вызвать DialogMessage.ProcessResult.
    /// </summary>
    /// <typeparam name="TDialog">Класс диалога. Должен иметь метод ShowDialog()</typeparam>
    public class DialogMessage<TDialog> where TDialog: CommonDialog
    {
        /// <summary>
        /// Результат отображения диалога.
        /// </summary>
        public class DialogMessageResult
        {
            public DialogMessageResult(TDialog dialog)
            {
                this.dialog = dialog;
            }

            /// <summary>
            /// Результат вызова метода DialogType.ShowDialog();
            /// </summary>
            public DialogResult DialogResult { get; set; }

            private TDialog dialog;

            /// <summary>
            /// Диалог. используется для извлечения результатов отображения (имя файла, папки, цвет и т.п.)
            /// </summary>
            public TDialog Dialog
            {
                get { return dialog; }
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dialog">Сконфигурированный диалог</param>
        /// <param name="resultCallback">Колбэк, который будет вызван после показа диалога.</param>
        public DialogMessage(object sender, TDialog dialog, Action<DialogMessageResult> resultCallback)
        {
            Sender = sender;
            this.Dialog = dialog;
            this._resultCallback = resultCallback;
            Result = new DialogMessageResult(Dialog);
        }

        /// <summary>
        /// Сам диалог. Используется принимающей сторонй для отображения.
        /// </summary>
        public TDialog Dialog { get; set; }

        /// <summary>
        /// Отправитель сообщения.
        /// </summary>
        public object Sender { get; set; }

        /// <summary>
        /// Результат отображения диалога.
        /// </summary>
        public DialogMessageResult Result { get; set; }

        /// <summary>
        /// Обработать результат. Вызывается принимающей стороной.
        /// </summary>
        public void ProcessResult()
        {
            _resultCallback(Result);
        }

        private readonly Action<DialogMessageResult> _resultCallback;
    }
}
