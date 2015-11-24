namespace WindowsTime.Infraestrutura.Framework.Aop
{
    public interface ICache
    {
        object this[string key] { get; }
        bool HasElement(string key);
        void AddInCache(string key, object cacheObject, int expirationMinutes);
        void RemoveItem(string key);
        void Clear();
    }
}