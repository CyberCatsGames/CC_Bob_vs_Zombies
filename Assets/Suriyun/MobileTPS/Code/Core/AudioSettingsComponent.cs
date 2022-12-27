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

       
       

      
    }
}