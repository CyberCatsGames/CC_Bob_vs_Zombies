using System;

namespace Suriyun.MobileTPS
{
    [Serializable]
    public abstract class PersistentProperty<TPropertyType> : ObservableProperty<TPropertyType>
    {
        protected TPropertyType Stored;
        private TPropertyType _defaultValue;

        public override TPropertyType Value
        {
            get => Stored;
            set
            {
                bool isEqual = Stored.Equals(value);
                if (isEqual == true) return;

                var oldValue = _value;
                Write(value);
                Stored = _value = value;

                InvokeChangedEvent(value, oldValue);
            }
        }

        protected PersistentProperty(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public void Validate()
        {
            if (Stored.Equals(_value) == false)
            {
                Value = _value;
            }
        }

        protected void Init()
        {
            Stored = _value = Read(_defaultValue);
        }

        protected abstract void Write(TPropertyType value);

        protected abstract TPropertyType Read(TPropertyType defaultValue);
    }
}