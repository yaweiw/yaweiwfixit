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
            _connectionString = "yaweiredis.redis.cache.windows.net:6380,password=wUntAQkzuwJ5bGnN19qtubF0TB0QwJu9wrHxPeDwyKM=,ssl=True,abortConnect=False";
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
             {
                 return ConnectionMultiplexer.Connect(_connectionString);
             });
        }
    }
}
