using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class Language : MonoBehaviour
    {
        public static Language Instance;

        public string CurrentLanguage;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                CurrentLanguage = GetLanguage();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [DllImport("__Internal")]
        private static extern string GetLanguage();
    }
}