using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RedisCache.CacheBusiness;
using RedisCache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCache.Controller
{
    
    [Route("[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        IBusiness _business;
        private readonly IConfiguration _configuration;

        public CacheController(IConfiguration configuration, IBusiness business)
        {
            _configuration = configuration;
            _business = business;
        }

        [HttpGet]
        [Route("/get/{key}")]
        public object GetCache(string key)
        {
            return _business.GetFromProvider(key);
        }

        [HttpPost]
        [Route("/set")]
        public bool SetCache(CacheObject cacheObj)
        {
            return _business.SetFromProvider(cacheObj);
        }

        [HttpPost]
        [Route("/remove/{key}")]
        public bool RemoveCache(string key)
        {
            return _business.RemoveFromProvider(key);
        }

    }
}

