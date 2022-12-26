using UnityEngine;

namespace Suriyun.MobileTPS.Code.Core
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSettingsComponent[] Backgrounds;
        public AudioSettingsComponent Sounds;

        private static AudioManager _instance;

        public static AudioManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
                return;

            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}