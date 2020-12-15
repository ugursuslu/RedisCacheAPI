using RedisCache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.CacheBusiness
{
    public interface IBusiness
    {
        object GetFromProvider(string key);
        bool SetFromProvider(CacheObject cacheObj);
        bool RemoveFromProvider(string key);
    }
}
