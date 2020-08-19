using System.Collections.Generic;
using CurrencyConverter.Shared.Utilities;

namespace CurrencyConverter.UI.Utilities.Settings
{
    public static class SettingsService
    {
        private static Dictionary<string, string> _values;
        private static string _file;

        static SettingsService()
        {
            _file = Constants.UserSettings;
            _values = new Dictionary<string, string>();
            DirectoryUtility.EnsureDirectoryExists(_file);
            Read();
        }

        private static void Save()
        {
            XMlUtil.Save(_file, _values);
        }

        private static void Read()
        {
            _values = XMlUtil.Read(_file);
            if (_values == null)
            {
                _values = new Dictionary<string, string>();
            }
        }

        public static string GetValue(string key)
        {
            if (_values != null &&
                _values.ContainsKey(key))
            {
                return _values[key];
            }
            return null;
        }

        public static void AddValue(string key, string value)
        {
            if (!_values.ContainsKey(key) ||
                _values[key] != value)
            {
                _values[key] = value;
                Save();
            }
        }

        public static void RemoveValue(string key)
        {
            if (_values.ContainsKey(key))
            {
                _values.Remove(key);
                Save();
            }
        }
    }
}