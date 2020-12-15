using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedisCache.CacheBusiness;
using RedisCache.CacheProvider;
using RedisCache.Controller;
using RedisCache.Model;
using System;

namespace CacheTest
{
    [TestClass]
    public class RedisCacheTest
    {

        CacheController cacheController;
        
        IBusiness cacheBusiness;

        [TestInitialize]
        public void Init()
        {
            //var myConfiguration = new Dictionary<string, string>();         

            var configuration = new ConfigurationBuilder()
                .Build();

            var redisConnection = new Microsoft.Extensions.Caching.StackExchangeRedis.RedisCache(new Microsoft.Extensions.Caching.StackExchangeRedis.RedisCacheOptions
            {
                Configuration = "127.0.0.1:6379",

            });

            cacheBusiness = new Business(new Provider(redisConnection));
            cacheController = new CacheController(configuration, cacheBusiness);
       
        }
     

        [TestMethod]
        public void SetCacheTests()
        {
            CacheObject obj = new CacheObject();
            obj.Key = "c2";
            obj.TTL = 0;
            obj.Object = @"{ ""Name"":""Ugur"", ""Surname"":""Suslu"", ""Age"":30 }";

            bool resultSet = cacheController.SetCache(obj);
            Assert.AreEqual(true, resultSet);

            obj.Key = "c5";
            obj.TTL = 200;
            obj.Object = @"{ ""Name"":""Emre"", ""Surname"":""Þahin"", ""Age"":35 }";

            resultSet = cacheController.SetCache(obj);
            Assert.AreEqual(true, resultSet);

            obj.Key = "";
            obj.TTL = 10;
            obj.Object = @"{ ""Name"":""Aziz"", ""Surname"":""Ulaþ Çelik"", ""Age"":40 }";

            var ex = Assert.ThrowsException<Exception>(() => cacheController.SetCache(obj));
            Assert.AreEqual("Object cannot be null", ex.Message);

            obj.Key = "";
            obj.TTL = 10;
            obj.Object = "";

            ex = Assert.ThrowsException<Exception>(() => cacheController.SetCache(obj));
            Assert.AreEqual("Object cannot be null", ex.Message);

        }

        [TestMethod]
        public void GetCacheTests()
        {
            string key = "c2";      // rediste olan bir key ile get yaparken
            var resultGet = cacheController.GetCache(key);
            Assert.IsNotNull(resultGet);

            key = "c3"; // rediste olmayan bir key ile get yaparken
            var ex = Assert.ThrowsException<Exception>(() => cacheController.GetCache(key));
            Assert.AreEqual("Key not found", ex.Message);

            key = ""; // key deðeri olmadan redise ulaþmaya çalýþtýðýnda             
            ex = Assert.ThrowsException<Exception>(() => cacheController.GetCache(key));
            Assert.AreEqual("Key cannot be null", ex.Message);
        }

        [TestMethod]
        public void RemoveCacheTests()
        {
            string key = "c2"; // rediste olan bir key ile remove yaparken
            bool resultSet = cacheController.RemoveCache(key);
            Assert.AreEqual(true, resultSet);

            key = "c150";   // rediste olmayan bir key ile remove yaparken
            var ex = Assert.ThrowsException<Exception>(() => cacheController.RemoveCache(key));
            Assert.AreEqual("Key not found", ex.Message);

            key = "";  // key deðeri olmadan redise ulaþmaya çalýþtýðýnda
            ex = Assert.ThrowsException<Exception>(() => cacheController.RemoveCache(key));
            Assert.AreEqual("Key cannot be null", ex.Message);

        }
    }
}

