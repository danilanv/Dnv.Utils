using System;
using System.IO;

namespace Dnv.Utils.DummyLogging
{
    public class ConsoleLogger: ILogger, IDisposable
    {
        private ConsoleColor _defaultColor;
        private FileStream _fileStream;
        private StreamWriter _streamWriter;

        public ConsoleLogger(string fileName)
        {
            _fileStream = new FileStream(fileName, FileMode.Append);
            _streamWriter = new StreamWriter(_fileStream);
            _streamWriter.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

            _defaultColor = Console.ForegroundColor;
        }

        public void Debug(int level, string message)
        {
            AddMessage(level, message);
        }

        public void Trace(int level, string message)
        {
            AddMessage(level, message);
        }

        public void Error(int level, string message)
        {
            AddMessage(level, message, ConsoleColor.DarkRed);
        }

        public void Info(int level, string message)
        {
            AddMessage(level, message);
        }

        public void Info(int level, string message, ConsoleColor color)
        {
            AddMessage(level, message, color);
        }

        private void AddMessage(int level, string message, ConsoleColor color)
        {
            var pref = "";
            for (int i = 0; i < level; i++)
                pref += " ";

            Console.ForegroundColor = color;
            Console.WriteLine(pref + message);
            Console.ForegroundColor = _defaultColor;

            _streamWriter.WriteLine(message);
        }

        private void AddMessage(int level, string message)
        {
            var pref = "";
            for (int i = 0; i < level; i++)
                pref += " ";

            Console.WriteLine(pref + message);
            _streamWriter.WriteLine(message);
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