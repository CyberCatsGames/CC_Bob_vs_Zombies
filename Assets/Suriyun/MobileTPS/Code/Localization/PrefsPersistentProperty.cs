namespace Suriyun.MobileTPS
{
    public abstract class PrefsPersistentProperty<TPropertyValue> : PersistentProperty<TPropertyValue>
    {
        protected readonly string Key;

        protected PrefsPersistentProperty(TPropertyValue defaultValue, string key) : base(defaultValue)
        {
            Key = key;
        }
    }
}