using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Model
{
    public class CacheObject
    {
        public string Key { get; set; }
        public int TTL { get; set; }
        public object Object { get; set; }
    }
}
