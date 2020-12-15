using RedisCache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.CacheProvider
{
    public interface IProvider
    {
        object Get(string key);

  
        bool Set(CacheObject cacheObj);

        bool Remove(string key);
    }
}
