using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Threading;

namespace Dnv.Utils.DummyLogging
{
    public class GuiLogger2 : ILogger, IDisposable
    {
        private readonly int _listMaxSize;
        private readonly Dispatcher _dispatcher;
        private readonly ObservableCollection<LogMessageViewModel> _items;
        private readonly ObservableCollection<LogMessageViewModel> _importantItems;
        private readonly ObservableCollection<LogMessageViewModel> _errorItems;
        private readonly Color _defaultColor;

        private FileStream _fileStream;
        private StreamWriter _streamWriter;

        enum MessagType
        {
            Debug,
            Trace,
            Info,
            Warn,
            Error
        }


        public GuiLogger2(Dispatcher dispatcher,
            ObservableCollection<LogMessageViewModel> items,
            string fileName,
            Color defaultColor,
            int listMaxSize) :
            this(dispatcher, items, null, null, fileName, defaultColor, listMaxSize)
        {
        }

        public GuiLogger2(Dispatcher dispatcher, ObservableCollection<LogMessageViewModel> items,
            ObservableCollection<LogMessageViewModel> importantItems, ObservableCollection<LogMessageViewModel> errorItems,
            string fileName, Color defaultColor, int listMaxSize)
        {
            _fileStream = new FileStream(fileName, FileMode.Append);
            _streamWriter = new StreamWriter(_fileStream);
            _streamWriter.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            _dispatcher = dispatcher;
            _items = items;

            _listMaxSize = listMaxSize;
            _importantItems = importantItems;
            _errorItems = errorItems;
            _defaultColor = defaultColor;
        }


        private void AddMessage(MessagType type, int level, string message)
        {
            var messageViewModel = new LogMessageViewModel()
            {
                //ckColor = color
            };

            var pref = "";
            for (int i = 0; i < level; i++)
                pref += " ";

            messageViewModel.Message = pref + message;

            var typeString = GetMessageTypeString(type);

            _dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new Action(() =>
                {
                    if (_importantItems != null)
                    {
                        if (type == MessagType.Info || type == MessagType.Warn)
                        {
                            AddMessageToList(_importantItems, message, messageViewModel, typeString);
                            return;
                        }
                    }

                    if (_errorItems != null)
                    {
                        if (type == MessagType.Error)
                        {
                            AddMessageToList(_errorItems, message, messageViewModel, typeString);
                            return;
                        }
                    }

                    AddMessageToList(_items, message, messageViewModel, typeString);
                }));
        }

        private void AddMessageToList(ObservableCollection<LogMessageViewModel> log, string message, LogMessageViewModel messageViewModel, string typeString)
        {
            log.Add(messageViewModel);
            if (log.Count > _listMaxSize)
                log.RemoveAt(0);

            _streamWriter.WriteLine(DateTime.Now.ToString("T") + " " + typeString + ": " + message);
            _streamWriter.Flush();
        }

        private string GetMessageTypeString(MessagType type)
        {
            switch (type)
            {
                case MessagType.Info:
                    return "[info]";
                case MessagType.Warn:
                    return "[warn]";
                case MessagType.Debug:
                    return "[debug]";
                case MessagType.Error:
                    return "[error]";
                case MessagType.Trace:
                    return "[trace]";
            }

            return "";
        }

        public void Debug(int level, string message)
        {
            AddMessage(MessagType.Debug, level, message);
        }

        public void Trace(int level, string message)
        {
            AddMessage(MessagType.Trace, level, message);
        }

        public void Error(int level, string message)
        {
            AddMessage(MessagType.Error, level, message);
        }

        public void Info(int level, string message)
        {
            AddMessage(MessagType.Info, level, message);
        }

        public void Info(int level, string message, ConsoleColor color)
        {
            AddMessage(MessagType.Info, level, message);
        }


        public void Warn(int level, string message)
        {
            AddMessage(MessagType.Warn, level, message);
        }

        public void Dispose()
        {
            _streamWriter.Flush();
            _fileStream.Flush();
            _fileStream.Close();
            _fileStream.Dispose();
        }
    }
}
