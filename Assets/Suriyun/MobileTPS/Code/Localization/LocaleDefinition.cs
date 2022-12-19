using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Suriyun.MobileTPS
{
    [CreateAssetMenu(menuName = "Definitions/LocaleDefinition", fileName = "LocaleDefinition")]
    public class LocaleDefinition : ScriptableObject
    {
        [SerializeField] private string _url;
        [SerializeField] private List<LocaleItem> _localeItems;

        private UnityWebRequest _request;

        public Dictionary<string, string> GetData()
        {
            return _localeItems.ToDictionary(item => item._key, item => item._value);
        }

        [ContextMenu("Update locale")]
        public void UpdateLocale()
        {
            if (_request != null)
                return;

            _localeItems.Clear();
            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += OnUpdateCompleted;
        }

        private void OnUpdateCompleted(AsyncOperation operation)
        {
            if (operation.isDone == true)
            {
                string[] rows = _request.downloadHandler.text.Split('\n');

                foreach (var row in rows)
                {
                    AddLocaleItem(row);
                }

                _request = null;
            }
        }

        private void AddLocaleItem(string row)
        {
            try
            {
                string[] parts = row.Split('\t');
                _localeItems.Add(new LocaleItem { _key = parts[0], _value = parts[1] });
            }
            catch (Exception e)
            {
                Debug.LogError($"Can't parse row: {row}.\n{e}");
            }
        }

        [Serializable]
        private class LocaleItem
        {
            public string _key;
            public string _value;
        }
    }
}