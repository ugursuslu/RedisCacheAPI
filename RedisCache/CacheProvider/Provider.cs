using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisCache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.CacheProvider
{
    public class Provider : IProvider
    {
        private readonly IDistributedCache _distributedCache;

        public Provider(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public object Get(string key)
        {
            var valueString = _distributedCache.GetString(key);
            if (!string.IsNullOrEmpty(valueString))
            {
                var valueObject = JsonConvert.DeserializeObject<string>(valueString);
                return valueObject;
            }
            return valueString;
        }

        public bool Remove(string key)
        {
            _distributedCache.Remove(key);
            return true;
        }

        public bool Set(CacheObject cacheObj)
        {
            var valueString = JsonConvert.SerializeObject(cacheObj.Object.ToString()); // Gökhan abiye sor
            //bool returnSetCache;
            if (cacheObj.TTL == 0)
            {
                _distributedCache.SetString(cacheObj.Key, valueString);
            }
            else
            {
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheObj.TTL) // Ne kadar süre redis üzerinde kalacak?
                };

                _distributedCache.SetString(cacheObj.Key, valueString, cacheOptions);
            }
            return true;
        }
    }
}
