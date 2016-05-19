using DynamicMethodHandleGenerators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicHandleGeneratorsConsumer
{
    class Program
    {
        const int iterations = 9999999;
        private static void InterfaceCall(IPeople interf, int x)
        {
            //interf.GetDetails();
            x = interf.Calculate(x);
        }
        private static void InstanceCall(People p, int x)
        {
            //p.GetDetails();
            x = p.Calculate(x);
        }

        private static void DelegateCall(People p, DynamicMethodHandle mh, int x)
        {
            //mh.DynamicMethod(p, null);
            x = (int)mh.DynamicMethod(p, new object[] { x });
        }
        static void Main(string[] args)
        {
            string sth_local = "";
            People p = new People("a", 10);
            IPeople ip = new People("b", 20);

            // jit
            //p.GetDetails();
            //ip.GetDetails();
            //DynamicMethodHandle dmh1 = MethodCache.GetCachedMethodHandleByCacheKey(p, "GetDetails");
            //DelegateCall(p, dmh1);

            int pr = p.Calculate(1);
            int ipr = ip.Calculate(1);
            DynamicMethodHandle dmh2 = MethodCache.GetCachedMethodHandleByCacheKey(p, "Calculate");
            int dpr = (int)dmh2.DynamicMethod(p, new object[1] { 1 });


            string name = null;
            Random rnd = new Random();
            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            for (int i = iterations; i >= 0; i--)
            {
                InstanceCall(p, 3);
                //p.Name = "a" + rnd.Next(100).ToString();
            }
            stopWatch1.Stop();
            TimeSpan ts1 = stopWatch1.Elapsed;
            string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts1.Hours, ts1.Minutes, ts1.Seconds,
    ts1.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime1);


            object nameojb;
            DynamicPropertyHandle<People> ph1 = PropertyCache<People>.GetCachedPropertyByCacheKey("Name");
            //DynamicMethodHandle dmh1 = MethodCache.GetCachedMethodHandleByCacheKey(p, "Calculate");
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            for (int i = iterations; i >= 0; i--)
            {
                DynamicMethodHandle dmh1 = MethodCache.GetCachedMethodHandleByCacheKey(p, "Calculate");
                DelegateCall(p, dmh1, 3);
                //nameojb = ph1.DynamicPropertyGet(p);
                //ph1.DynamicPropertySet(p, "a" + rnd.Next(100).ToString());
            }
            stopWatch2.Stop();
            TimeSpan ts2 = stopWatch2.Elapsed;
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts2.Hours, ts2.Minutes, ts2.Seconds,
    ts2.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime2);

            Stopwatch stopWatch3 = new Stopwatch();
            stopWatch3.Start();
            for (int i = iterations; i >= 0; i--)
            {
                //InterfaceCall(ip, 3);
            }
            stopWatch3.Stop();
            TimeSpan ts3 = stopWatch3.Elapsed;
            string elapsedTime3 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts3.Hours, ts3.Minutes, ts3.Seconds,
    ts3.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime3);


            Console.ReadLine();
        }
    }
}
