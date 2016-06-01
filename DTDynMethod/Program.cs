using System;
using System.Reflection;
using System.Diagnostics;
using DynamicMethodHandleGenerators;

namespace DTDynMethod
{
    class Handle
    {
        public string Name { get; set; }
        public Handle(string name)
        {
            Name = name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string sth_local = "";
            People p = new People("a", 10);
            Type t = p.GetType();
            PropertyInfo pinfo = t.GetProperty("Name");
            Handle h = new Handle(pinfo.Name);
            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            for (int i = 9999999; i >= 0; i--)
            {
                string s = h.Name;
            }
            stopWatch1.Stop();
            TimeSpan ts1 = stopWatch1.Elapsed;
            string elapsedTime1 = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts1.Hours, ts1.Minutes, ts1.Seconds,
    ts1.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime1);

            //        DynamicMethodHandle dmh = MethodCache.GetCachedMethodHandleByCacheKey(p, "GetDetails");
            //        Func<object, object[], object> dm = dmh.DynamicMethod;
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            for (int i = 9999999; i >= 0; i--)
            {
                string s = pinfo.Name;
                //p.sth = "a";
                //p.sth = "b";
            }
            stopWatch2.Stop();
            TimeSpan ts2 = stopWatch2.Elapsed;
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts2.Hours, ts2.Minutes, ts2.Seconds,
    ts2.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime2);

            //        Stopwatch stopWatch3 = new Stopwatch();
            //        stopWatch3.Start();
            //        //sth_local = "a";
            //        //sth_local = "b";
            //        stopWatch3.Stop();
            //        TimeSpan ts3 = stopWatch3.Elapsed;
            //        string elapsedTime3 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //ts3.Hours, ts3.Minutes, ts3.Seconds,
            //ts3.Milliseconds / 10);
            //        Console.WriteLine("RunTime " + elapsedTime3);


            Console.ReadLine();

        }

    }
}
