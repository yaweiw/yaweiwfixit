namespace Timer
{
    using System;
    using System.Linq;
    using System.Reflection;
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
        public static void Main()
        {
            Derived d = new Derived();
            PropertyInfo[] props = d.Properties;
            string sc = GetDetails("abab", 'c'); 
            string cs = GetDetails('c' , "abab");
            Task mytask = new Task(() => Console.WriteLine("hi"));
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e)=> Console.WriteLine("hi"));
            // Set the Interval to 5 seconds.
            aTimer.Interval = 500;
            aTimer.Enabled = true;

            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

    }
}
