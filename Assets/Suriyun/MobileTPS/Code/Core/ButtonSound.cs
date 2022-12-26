using UnityEngine;
using UnityEngine.EventSystems;

namespace Suriyun.MobileTPS.Code.Core
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _clip;

        private AudioSource _source;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_source == null)
            {
                _source = GameObject.FindWithTag(AudioUtils.SfxAudioSource).GetComponent<AudioSource>();
            }

            _source.PlayOneShot(_clip);
        }
    }
}