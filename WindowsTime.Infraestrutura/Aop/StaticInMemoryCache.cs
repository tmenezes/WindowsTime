using System;
using System.Runtime.Caching;

namespace WindowsTime.Infraestrutura.Aop
{
    public class StaticInMemoryCache : ICache
    {
        private static readonly ObjectCache _inMemoryCache;

        // constructor
        static StaticInMemoryCache()
        {
            _inMemoryCache = MemoryCache.Default;
        }


        // public
        public object this[string key]
        {
            get { return HasElement(key) ? _inMemoryCache.Get(key) : null; }
        }

        public bool HasElement(string key)
        {
            return _inMemoryCache.Contains(key);
        }

        public void AddInCache(string key, object cacheObject, int expirationMinutes)
        {
            if (HasElement(key))
                throw new InvalidOperationException("Cached object's Key already exists.");


            var cip = new CacheItemPolicy()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(expirationMinutes))
            };

            _inMemoryCache.Set(key, cacheObject, cip);
        }

        public void RemoveItem(string key)
        {
            if (HasElement(key))
            {
                _inMemoryCache.Remove(key);
            }
        }

        public void Clear()
        {

        }
    }
}
