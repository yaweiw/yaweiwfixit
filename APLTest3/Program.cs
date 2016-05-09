using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLTest3
{
    class Program
    {
        static async Task call()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                string ret = "";
                Task.Delay(10000);
                Console.WriteLine("task 1");
                ret = "task 1";
                return ret;
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                string ret = "";
                Task.Delay(1000);
                Console.WriteLine("task 2");
                ret = "task 2";
                return ret;
            });
            await Task.WhenAll(t1, t2);
            Console.WriteLine("t1.Result:" + t1.Result);
            Console.WriteLine("t2.Result:" + t2.Result);
            Console.WriteLine("finish call");
        }

        static void Main(string[] args)
        {
            call();
            Console.ReadLine();
        }
    }
}
