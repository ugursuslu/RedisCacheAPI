using RedisCache.CacheProvider;
using RedisCache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.CacheBusiness
{
    public class Business : IBusiness
    {
        IProvider _provider;
        public Business(IProvider provider)
        {
            _provider = provider;
        }
        public object GetFromProvider(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var returnValue = _provider.Get(key);
                if (returnValue == null)
                {
                    throw new Exception("Key not found");
                }
                return returnValue;
            }
            else
            {
                throw new Exception("Key cannot be null");
            }
        }

        public bool RemoveFromProvider(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                var returnValue = _provider.Get(key);
                if (returnValue == null)
                {
                    throw new Exception("Key not found");
                }
                return _provider.Remove(key);
            }
            else
            {
                throw new Exception("Key cannot be null");
            }
        }

        public bool SetFromProvider(CacheObject cacheObj)
        {
            if (!string.IsNullOrEmpty(cacheObj.Key) && !string.IsNullOrEmpty(cacheObj.Object.ToString()))
            {
                return _provider.Set(cacheObj);
            }
            else
            {
                throw new Exception("Object cannot be null");
            }
        }
    }
}
