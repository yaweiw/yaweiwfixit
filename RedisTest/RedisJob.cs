using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisTest
{
    public class RedisJob
    {
        private readonly int _maxParallelism;
        private readonly IDatabase _database;
        private readonly IServer _server;

        public RedisJob(IServer server, IDatabase database, int maxParallelism)
        {
            _server = server;
            _database = database;
            _maxParallelism = maxParallelism;
        }

        private static async Task<TResult> SelectCoreAsync<TSource, TResult>(Func<TSource, Task<TResult>> body, SemaphoreSlim semaphore, TSource s)
        {
            await semaphore.WaitAsync();
            try
            {
                return await body(s);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task FlushDbJobAsync()
        {
            await _server.FlushDatabaseAsync();
        }

        public async Task SetJobAsync(Dictionary<string,string> dict)
        {
            Func<KeyValuePair<string, string>, Task<bool>> setjob = async p => await _database.StringSetAsync(p.Key, p.Value);
            using (var semaphore = new SemaphoreSlim(_maxParallelism))
            {
                // warning "access to disposed closure" around "semaphore" could be ignored as it is inside Task.WhenAll
                await Task.WhenAll(from s in dict select SelectCoreAsync(setjob, semaphore, s));
            }
        }

        public async Task GetJobAsync(Dictionary<string, string>.KeyCollection keys, string value)
        {
            Func<string, Task<string>> getjob = async (key) =>
            {
                string result = await _database.StringGetAsync(key);
                if (result == value)
                {
                    Console.WriteLine($"matched! {key}, {value}");
                }
                return result;
            };
            using (var semaphore = new SemaphoreSlim(_maxParallelism))
            {
                // warning "access to disposed closure" around "semaphore" could be ignored as it is inside Task.WhenAll
                await Task.WhenAll(from key in keys select SelectCoreAsync(getjob, semaphore, key));
            }
        }

        public async Task SetBatchJobWrapAsync(Dictionary<string, string> dict)
        {

        }

        public async Task SetBatchJobAsync(Dictionary<string, string> subset)
        {
            List<Task<bool>> list = new List<Task<bool>>();
            IBatch batch = _database.CreateBatch();
            foreach (var pair in subset)
            {
                var task = batch.StringSetAsync(pair.Key, pair.Value);
                list.Add(task);
            }
            batch.Execute();
            return;
        }
    }
}
