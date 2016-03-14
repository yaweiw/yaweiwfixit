using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLTest2
{
    class Program
    {
        static async Task<bool> M3()
        {
            Console.WriteLine("4 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(500);
            Console.WriteLine("6 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return true;
        }

        static async Task<int> M2()
        {
            Console.WriteLine("3 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            Task<bool> m3task = M3();
            await m3task;
            Console.WriteLine("7 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            return 1;
        }

        static async Task M1()
        {
            Console.WriteLine("2 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            Task<int> m2task = M2();
            await m2task;
            Console.WriteLine("8 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        }

        // Two cases:
        // 1. M1 returns void; M1() => event handler fire and forget, doesn't care if any call stie returnes
        // 2. M1 returns Task; Task t = M1(); t.Wait(). It waits until the inner most call site returns.
        //
        static void Main(string[] args)
        {
            while (Console.ReadLine() != "e")
            {
                Console.WriteLine("1 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                // case 1:
                // M1();
                // case 2:
                Task t = M1();
                t.Wait();
                Console.WriteLine("5 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                // works like a event handler fire and forget
                // control returns here immediately
            }
        }
    }
}
