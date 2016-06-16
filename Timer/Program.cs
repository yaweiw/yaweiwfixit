namespace Timer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Timers;
    using System.Web;

    public abstract class BaseClass
    {
        private static System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]> _dictionary = new System.Collections.Concurrent.ConcurrentDictionary<Type, PropertyInfo[]>();

        public PropertyInfo[] Properties
        {
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

        public Derived(string _abab = "abab", string[] _acac = null)
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
            if (string.IsNullOrEmpty(path))
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

        private static async Task<int> caller(Func<int,Task<int>> fun, int j)
        {
            return await fun.Invoke(j);

        }

        private static async Task<int> DoAsync(int i)
        {
            await Task.Delay(i);
            return i + 10;
        }
        public static void Main()
        {
            Func<int, Task<int>> valueSelector = (a) => DoAsync(a);
            Task<int> c = caller(async (i) => await DoAsync(i), 5);
            c.Wait();
            int j = c.Result;
            string pat = @"(?<left><)(?<middle>.*)(?<right>>)";
            string s = @"Unable to resolve [!include[<title>](task-snippetsinclude[<title>](task-/tasksinclude[<title>](task-/Appinclude[<title>](task--version-and-title.md)] :Could not find a part of the path 'W:\32wuvr1w.2bw\source\windows-snippets\test\task-snippets\tasks\App-version-and-title.md'.";

            Func<string> del = () => { return pat + s; };
            var ret = del.Invoke();

            string ss = HttpUtility.HtmlEncode(s);
            Console.ReadLine();
        }
    }
}


