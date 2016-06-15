using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLTest3
{
    class Program
    {
        static async Task<int> TaskOfT_MethodAsync()
        {
            Task t = Task.Delay(5000);
            // The body of the method is expected to contain an awaited asynchronous
            // call.
            // Task.FromResult is a placeholder for actual work that returns a string.
            var today = await Task.FromResult<string>(DateTime.Now.DayOfWeek.ToString());

            // The method then can process the result in some way.
            int leisureHours;
            if (today.First() == 'S')
                leisureHours = 16;
            else
                leisureHours = 5;

            await t;
            // Because the return statement specifies an operand of type int, the
            // method must have a return type of Task<int>.
            return leisureHours;
        }
        static async Task call()
        {
            // Call and await the Task<T>-returning async method in the same statement.
            //int result1 = await TaskOfT_MethodAsync();

            // Call and await in separate statements.
            Task<int> integerTask = TaskOfT_MethodAsync();

            // You can do other work that does not rely on integerTask before awaiting.
            await Task.Delay(100);

            //int result2 = await integerTask;
            int result2 = integerTask.Result;


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
