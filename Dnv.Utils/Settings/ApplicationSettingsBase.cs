using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Dnv.Utils.Settings
{
    public interface IApplicationSettingsBase
    {
        /// <summary>
        /// First application run.
        /// </summary>
        bool IsFirstInitialization { get; }

        /// <summary>
        /// Save settings.
        /// </summary>
        void Save();
    }

    public class ApplicationSettingsBase
    {
        static private string _applicationDataPath = string.Empty;

        /// <summary>
        /// Директория для хранения локальных настроек приложения текущего пользователя. %LOCALAPPDATA%\<company>\<application>
        /// Если директория не существует, то она будет создана.
        /// </summary>
        static public string LocalApplicationDataPath
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationDataPath))
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var company = (AssemblyCompanyAttribute)Attribute.
                        GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
                    var product = (AssemblyProductAttribute)Attribute.
                        GetCustomAttribute(assembly, typeof(AssemblyProductAttribute));

                    _applicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData,
                                                        Environment.SpecialFolderOption.Create);
                    _applicationDataPath = Path.Combine(_applicationDataPath, company.Company);
                    _applicationDataPath = Path.Combine(_applicationDataPath, product.Product);
                    if (!Directory.Exists(_applicationDataPath))
                        Directory.CreateDirectory(_applicationDataPath);
                }

                return _applicationDataPath;
            }
        }
    }

    /// <summary>
    /// Базовый класс для управления настройками приложения. 
    /// </summary>
    /// <typeparam name="TSettingsPdo">PDO класс используемый для сохранения настроек. 
    /// Должен содержать только свойства.</typeparam>
    public class ApplicationSettingsBase<TSettingsPdo> :ApplicationSettingsBase,  IApplicationSettingsBase where TSettingsPdo : class, new()
    {
        private readonly bool _isFirstInitialization = false;

        private readonly string _settingsFileName;

        /// <summary>
        /// PDO класс используемый для сохранения настроек. 
        /// </summary>
        protected TSettingsPdo SettingsPdo = null;


        /// <summary>
        /// Конструктор. Создаёт файл настроек или загружает его если он имеется. 
        /// Название компании и продукта берутся из информации о сборке приложения, 
        /// которое загрузило эту сборку. </summary>
        /// <param name="settingsFileName">Имя файла настроек.</param>
        public ApplicationSettingsBase(string settingsFileName)
        {
            _settingsFileName = settingsFileName;

            if (SettingsPdo == null)
            {
                if (File.Exists(SettingsFullFilePath))
                    SettingsPdo = JsonConvert.DeserializeObject<TSettingsPdo>(File.ReadAllText(SettingsFullFilePath));
                else
                {
                    _isFirstInitialization = true;
                    SettingsPdo = new TSettingsPdo();
                    Save();
                }
            }
        }

        protected string SettingsFullFilePath
        {
            get { return LocalApplicationDataPath + "\\" + _settingsFileName; }
        }

        public bool IsFirstInitialization
        {
            get { return _isFirstInitialization; }
        }

        public void Save()
        {
            var outputJson = JsonConvert.SerializeObject(SettingsPdo, Formatting.Indented);
            File.WriteAllText(SettingsFullFilePath, outputJson);
        }
    }
}
