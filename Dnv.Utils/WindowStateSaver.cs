using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace Dnv.Utils
{
    /// <summary>
    /// Автоматически сохраняет/загружает состояние окна (размер, положение, состояние).
    /// </summary>
    public class WindowStateSaver: IDisposable
    {
        readonly Window _window;

        Dictionary<string, string> _dict = new Dictionary<string, string>();

        readonly string _fileName;


        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="window">Окно, состояние которого нужно сохранять/загружать.</param>
        /// <param name="fileName">Файл состояния.</param>
        public WindowStateSaver(Window window, string fileName)
        {
            _window = window;
            _fileName = fileName;

            _window.Closing += WindowOnClosing;
            _window.Loaded += WindowOnLoaded;  
        }

        private void WindowOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Load();
        }

        private void WindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            Save();
        }

        private void Save()
        {
            _dict.Clear();

            _dict.Add("width", _window.ActualWidth.ToString());
            _dict.Add("height", _window.ActualHeight.ToString());
            _dict.Add("left", _window.Left.ToString());
            _dict.Add("top", _window.Top.ToString());
            _dict.Add("state", ((int)_window.WindowState).ToString());

            File.WriteAllText(_fileName, JsonConvert.SerializeObject(_dict, Formatting.Indented));
        }

        private void Load()        
        {
            if (!File.Exists(_fileName))
                return;

            _dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_fileName));


            if (_dict.ContainsKey("width"))
                _window.Width = double.Parse(_dict["width"]);

            if (_dict.ContainsKey("height"))
                _window.Height = double.Parse(_dict["height"]);

            if (_dict.ContainsKey("left"))
                _window.Left = double.Parse(_dict["left"]);

            if (_dict.ContainsKey("top"))
                _window.Top = double.Parse(_dict["top"]);

            if (_dict.ContainsKey("state"))
                _window.WindowState = (WindowState)int.Parse(_dict["state"]);            
        }

        public void Dispose()
        {
            _window.Closing -= WindowOnClosing;
            _window.Loaded -= WindowOnLoaded;
        }
    }
}
