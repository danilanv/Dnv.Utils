using System;
using System.Windows.Forms;

namespace Dnv.Utils.Messages
{
    /// <summary>
    /// Message for using with MVVMLight. Allows view model to tell view to show the dialog and get dialog result.
    /// Receving view should show dialog and call DialogMessage.ProcessResult.
    /// </summary>
    /// <typeparam name="TDialog">Type of dialog. Should have method ShowDialog()</typeparam>
    /// <example>
    /// <code> 
    /// ViewModel:
    ///  var msg = new DialogMessage<FolderBrowserDialog>(this,          
    ///  new FolderBrowserDialog()          
    ///  {          
    ///      ShowNewFolderButton = false,          
    ///      SelectedPath = LastPath          
    ///  },          
    ///  result =>          
    ///  {  
    ///      if (result.DialogResult == DialogResult.OK)  
    ///         browseResult(result.Dialog.SelectedPath);  
    ///  });  
    ///  Messenger.Default.Send(msg, AppMessages.ShowSelectDialog);
    /// 
    /// View:
    /// Messenger.Default.Register<DialogMessage<FolderBrowserDialog>>(this, AppMessages.ShowSelectDialog,
    ///        (msg) =>
    ///        {    
    ///            msg.Result.DialogResult = msg.Dialog.ShowDialog();    
    ///            msg.ProcessResult();    
    ///        });    
    /// </code>
    /// </example>
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
            Dialog = dialog;
            _resultCallback = resultCallback;
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
