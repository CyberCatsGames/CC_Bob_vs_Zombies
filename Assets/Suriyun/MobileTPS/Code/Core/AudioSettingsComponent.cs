using UnityEngine;

namespace Suriyun.MobileTPS.Code.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public void TurnOn()
        {
            _source.mute = false;
        }

        public void TurnOff()
        {
            _source.mute = true;
        }

        public void BtnPlaySoundDelay(float delay) => Invoke(nameof(PlaySound), delay);
        private void PlaySound() => _source.Play();
        public void BtnStopSoundDelay(float delay) => Invoke(nameof(StopSound), delay);
        private void StopSound() => _source.Stop();

       

      
    }
}