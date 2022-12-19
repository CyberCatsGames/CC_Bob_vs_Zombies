using System;
using UnityEngine;

namespace Suriyun.MobileTPS
{
    [Serializable]
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);

        public event OnPropertyChanged OnChanged;

        public IDisposable Subscribe(OnPropertyChanged call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public IDisposable SubscribeAndInvoke(OnPropertyChanged call)
        {
            OnChanged += call;
            OnChanged?.Invoke(_value, _value);
            return new ActionDisposable(() => OnChanged -= call);
        }

        public virtual TPropertyType Value
        {
            get => _value;
            set
            {
                bool isSame = false;
                if (_value != null)
                    isSame = _value.Equals(value);
                if (isSame == true) return;

                var oldValue = _value;
                _value = value;
                OnChanged?.Invoke(_value, oldValue);
            }
        }

        protected void InvokeChangedEvent(TPropertyType value, TPropertyType oldValue)
        {
            OnChanged?.Invoke(value, oldValue);
        }
    }
}