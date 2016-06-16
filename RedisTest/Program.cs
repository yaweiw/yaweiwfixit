using System;

namespace RedisTest
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// args[0]: metadata80
        /// args[1]: d:\temp
        /// args[2]: redis
        /// args[3]: parallelsim
        static void Main(string[] args)
        {
            if (args[2] == "redis")
            {
                Console.WriteLine("redis: Press Any Key To Start...");
                Console.ReadLine();
                RedisTest rt = new RedisTest();
                rt.Test(args[0], args[1], Convert.ToInt32(args[3]));
                Console.WriteLine("redis");
            }
            else if (args[2] == "blob")
            {
                Console.WriteLine("blob: Press Any Key To Start...");
                Console.ReadLine();
                while (true)
                {
                    BlobTest bt = new BlobTest(args[1]);
                    bt.Test(args[0], args[1]);
                }
                // Console.WriteLine("blob");
            }
            else
            {
            }
        }
    }
}
