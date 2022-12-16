using System;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;

        private float _timesUp;

        public bool IsReady => _timesUp <= Time.time;

        public void Reset()
        {
            _timesUp = _value + Time.time;
        }
    }
}