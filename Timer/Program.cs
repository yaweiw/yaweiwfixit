namespace Timer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Timers;

    public abstract class BaseClass
    {
        private static System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]> _dictionary = new System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]>();

        public PropertyInfo[] Properties {
            get
            {
                Type key = GetType();
                PropertyInfo[] properties = null;
                if (!_dictionary.TryGetValue(key, out properties))
                {
                    properties = GetType().GetProperties()
                .Where(property => property.GetIndexParameters().Length == 0 && property.GetGetMethod() != null && property.GetSetMethod() != null).ToArray();
                    _dictionary.TryAdd(key, properties);
                }
                return properties;
            }
        }
        public BaseClass()
        {
        }
    }

    public class Derived : BaseClass
    {
        public string abab { get; set; } 

        public string[] acac { get; set; }
        public string Str { get; set; }
        public int Inter { get; set; }

        public string Str2 { private get; set; }
        public int Inter2 { get; private set; }

        public Derived(string _abab = "abab", string[] _acac = null )
        {
            abab = _abab;
            acac = _acac == null ? new string[0] : _acac;
        }
    }

    public class Timer1
    {
        public static string GetDetails(string s)
        {
            return "s";
        }
        public static string GetDetails(char c)
        {
            return "c";
        }
        public static string GetDetails(char c, string s)
        {
            return "cs";
        }
        public static string GetDetails(string s, char c)
        {
            return "sc";
        }

        private static Stream SafeOpen(string path)
        {
            if(string.IsNullOrEmpty(path))
            {
                return null;
            }
            try
            {
                return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);
            }
            catch
            {
                return null;
            }
        }

        public class ABC
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public ABC(string a, string b, string c)
            {
                A = a;
                B = b;
                C = c;
            }
        }

        public static void Main()
        {
            List<ABC> list1 = new List<ABC>();
            list1.Add(new ABC("a1", "b1", "c1"));
            list1.Add(new ABC("a2", "b2", "c2"));
            list1.Add(new ABC("a3", "b3", "c3"));
            list1.Add(new ABC("a4", "b4", "c4"));
            list1.Add(new ABC("aa4", "bb4", "cc4"));

            List<ABC> list2 = new List<ABC>();
            list2.Add(new ABC("a1", "b1", "c1"));
            list2.Add(new ABC("a2", "b2", "c2"));
            list2.Add(new ABC("a3", "b3", "c3"));
            list2.Add(new ABC("a4", "b4", "c4"));

            var diff = list1.Select(list1item => new { list1item.A, list1item.C }).Except(list2.Select(list2item => new { list2item.A, list2item.C }));


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["abab"] = "abab";
            Console.WriteLine(dic.Count());

            using (var stream = SafeOpen(@"d:\sample.html"))
            using (var stream2 = SafeOpen(@"d:\toremove.txt"))
            {

                using (var md5 = MD5.Create())
                {
                    var hash = md5.ComputeHash(stream);
                    long pos = stream.Position;
                }
                Console.WriteLine(stream?.Length);
                if(stream?.Equals(stream2) == false || stream?.Equals(stream2) == null)
                {
                    Console.WriteLine("abab");
                }
            }

            Derived d = new Derived();
            PropertyInfo[] props = d.Properties;
            string sc = GetDetails("abab", 'c'); 
            string cs = GetDetails('c' , "abab");
            Task mytask = new Task(() => Console.WriteLine("hi"));
            Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e)=> Console.WriteLine("hi"));
            // Set the Interval to 5 seconds.
            aTimer.Interval = 500;
            aTimer.Enabled = true;

            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

    }
}
