using StackExchange.Redis;
using System;

namespace RedisTest
{
    public class TestRedisCacheConfiguration
    {
        private readonly string _connectionString;

        private Lazy<ConnectionMultiplexer> _lazyConnection;

        public ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }

        public TestRedisCacheConfiguration()
        {
            _connectionString = "publisheddocscache.redis.cache.windows.net:6380,password=WyhFaAaL07ZiX+k8l/ENgi5t1eGyeaAzc00nQAUncy8=,ssl=True,abortConnect=False";
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
             {
                 return ConnectionMultiplexer.Connect(_connectionString);
             });
        }
    }
}
