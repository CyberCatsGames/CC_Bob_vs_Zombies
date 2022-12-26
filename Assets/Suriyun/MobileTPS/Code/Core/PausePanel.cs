using UnityEngine;
using UnityEngine.UI;

namespace Suriyun.MobileTPS.Code.Core
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private Toggle _music;
        [SerializeField] private Toggle _sounds;

        private void OnEnable()
        {
            _music.onValueChanged.AddListener(OnMusicChanged);
            _sounds.onValueChanged.AddListener(OnSoundsChanged);
        }

        private void OnDisable()
        {
            _music.onValueChanged.RemoveListener(OnMusicChanged);
            _sounds.onValueChanged.RemoveListener(OnSoundsChanged);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        private void OnSoundsChanged(bool value)
        {
            if (value == true)
            {
                AudioManager.Instance.Sounds.TurnOn();
            }
            else
            {
                AudioManager.Instance.Sounds.TurnOff();
            }
        }

        private void OnMusicChanged(bool value)
        {
            if (value == true)
            {
                foreach (var component in AudioManager.Instance.Backgrounds)
                {
                    component.TurnOn();
                }
            }
            else
            {
                foreach (var component in AudioManager.Instance.Backgrounds)
                {
                    component.TurnOff();
                }
            }
        }
    }

    public static class AudioUtils
    {
        public static string SfxAudioSource => "SfxAudioSource";
        public static string Music => "Music";
    }
}