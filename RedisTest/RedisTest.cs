using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    public class RedisTest
    {
        public void Test(string metadatafile)
        {
            TestRedisCacheConfiguration config = new TestRedisCacheConfiguration();
            ConnectionMultiplexer connection = config.Connection;
            var database = connection.GetDatabase();
            var endpoints = connection.GetEndPoints();
            var server = connection.GetServer(endpoints[0]);
            RedisJob job = new RedisJob(server, database, 256);

            Stopwatch stopWatch0 = new Stopwatch();
            stopWatch0.Start();
            // generate 80k dict
            Helper.LoadJson(@"D:\temp\" + metadatafile + ".json");
            if (Helper.Dict.Count == 0)
            {
                Console.WriteLine("Load Json file failed.");
                return;
            }
            stopWatch0.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts0 = stopWatch0.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime0 = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts0.Hours, ts0.Minutes, ts0.Seconds,
                ts0.Milliseconds / 10);
            Console.WriteLine("Load Json RunTime: " + elapsedTime0);


            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            Task s1 = job.SetJobAsync(Helper.Dict);
            s1.Wait();
            stopWatch1.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts1 = stopWatch1.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime1 = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts1.Hours, ts1.Minutes, ts1.Seconds,
                ts1.Milliseconds / 10);
            Console.WriteLine("Set 10k records: RunTime: " + elapsedTime1);


            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            Task s2 = job.GetJobAsync(Helper.Dict.Keys, "e1f6dd6b-7c97-430b-b9ca-5864d1b143a78JOUA3TU8EEI0Z00P07458D4KUWY67910XMK9G9GK4SEEQOYZSEZ7Y7AGTKQH5XB");
            //
            //publishing work skiped
            //
            s2.Wait();
            stopWatch2.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts2 = stopWatch2.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts2.Hours, ts2.Minutes, ts2.Seconds,
                ts2.Milliseconds / 10);
            Console.WriteLine("Get 10k records: RunTime: " + elapsedTime);
        }
    }
}
