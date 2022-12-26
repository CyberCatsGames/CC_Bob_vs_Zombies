using System;
using UnityEngine;

namespace Suriyun.MobileTPS.Code.Core
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioData[] _sounds;
        [SerializeField] private bool _playOnEnable;

        private AudioSource _source;

        private void Awake()
        {
            _source = GameObject.FindWithTag(AudioUtils.SfxAudioSource).GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (_playOnEnable)
                foreach (var data in _sounds)
                {
                    _source.PlayOneShot(data.Clip);
                }
        }

        public void Play(string id)
        {
            foreach (var sound in _sounds)
            {
                if (sound.Id == id)
                {
                    _source.PlayOneShot(sound.Clip);
                    break;
                }
            }
        }

        [Serializable]
        private class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;

            public AudioClip Clip => _clip;
        }
    }
}