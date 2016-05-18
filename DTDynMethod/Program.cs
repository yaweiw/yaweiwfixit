using System;
using System.Reflection;
using System.Diagnostics;

namespace DTDynMethod
{
    public class People
    {
        public string sth;
        public string Name { get; set; }
        public uint Age { get; set; }
        public People() : this(string.Empty, 0)
        {
        }
        public People(string name, uint age)
        {
            sth = name;
            Name = name;
            Age = age;
        }
        public string GetDetails()
        {
            return "Name is: " + Name + ". Age is: " + Age.ToString() + ".";
        }
        public string GetDetails(int age)
        {
            return "s";
        }
        public string GetDetails(char c)
        {
            return "s";
        }

    }
    class Program
    {
        private const BindingFlags ctorFlags
            = BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.NonPublic
            ;
        string Name => @"a program";
        Uri URI => new Uri(@"http://abab/");
        static void Main(string[] args)
        {
            string sth_local = "";

            People p = new People("a", 10);
            string s = p.GetDetails('c');
            string ss = p.GetDetails(12);
            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            for (int i = 999999999; i >= 0; i--)
            {
                p.Name = "a";
                p.Name = "b";
            }
            stopWatch1.Stop();
            TimeSpan ts1 = stopWatch1.Elapsed;
            string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts1.Hours, ts1.Minutes, ts1.Seconds,
    ts1.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime1);

            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            for (int i = 999999999; i >= 0; i--)
            {
                p.sth = "a";
                p.sth = "b";
            }
            stopWatch2.Stop();
            TimeSpan ts2 = stopWatch2.Elapsed;
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts2.Hours, ts2.Minutes, ts2.Seconds,
    ts2.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime2);

            Stopwatch stopWatch3 = new Stopwatch();
            stopWatch3.Start();
            sth_local = "a";
            sth_local = "b";
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
