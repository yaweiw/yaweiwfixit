using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task<TResult> RunGeneric<TResult>(Task<TResult> task, Func<TResult,string> messageprocessor)
    {
        Console.WriteLine("5 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        int x = 2;
        int y = 3;
        Console.WriteLine(x + y);
        Console.WriteLine("6 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        TResult result = await task;
        Console.WriteLine("8 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        string processedmsg = messageprocessor(result);
        Console.WriteLine("10 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        Console.WriteLine(processedmsg);
        return result;
    }

    static async Task<string> FirstTask()
    {
        Console.WriteLine("3 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        int i = 100;
        while (i-- > 0) ;
        Console.WriteLine("4 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        //Task.Delay(5000).Wait();
        await Task.Delay(5000);
        Console.WriteLine("7 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        return @"FirstTaks finishes";

    }

    static async void RunMainAsync()
    {
        Console.WriteLine("2 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        Task<string> t = RunGeneric<string>(
            FirstTask(),
            (srcmsg) =>
            {
                Console.WriteLine("9 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
                return srcmsg.ToUpper();
            }
            );
        await t;
    }
    static void Main()
    {
        Console.WriteLine("1 " + System.Threading.Thread.CurrentThread.ManagedThreadId);
        RunMainAsync();
        //control returns here because of async void 
        Console.ReadLine();
    }
}