using System;

namespace RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadLine();
            //RedisTest rt = new RedisTest();
            //rt.Test("metadata1k");
            //Console.ReadLine();

            BlobTest bt = new BlobTest();
            bt.Test("metadata80k");
        }
    }
}
