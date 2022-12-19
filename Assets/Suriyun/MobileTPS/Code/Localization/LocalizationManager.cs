using System;
using System.Collections.Generic;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class LocalizationManager
    {
        private Dictionary<string, string> _localizationDictionary;

        private readonly StringPersistentProperty _currentLocale = new("en", "localization/current");

        public static readonly LocalizationManager Instance;

        public string LocalKey => _currentLocale.Value;

        public event Action OnLocaleChanged;

        static LocalizationManager()
        {
            Instance = new LocalizationManager();
        }

        private LocalizationManager()
        {
            LoadLocaleDefinition(_currentLocale.Value);
        }

        private void LoadLocaleDefinition(string localeToLoad)
        {
            var localeDefinition = Resources.Load<LocaleDefinition>($"Locales/{localeToLoad}");
            _localizationDictionary = localeDefinition.GetData();
            OnLocaleChanged?.Invoke();
        }

        public string GetLocalization(string key)
        {
            return _localizationDictionary.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        public void SetLocale(string selectedLocale)
        {
            _currentLocale.Value = selectedLocale;
            LoadLocaleDefinition(_currentLocale.Value);
        }
    }
}