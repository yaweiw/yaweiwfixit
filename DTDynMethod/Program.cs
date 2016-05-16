using System;
using DynamicMethodHandlerFactoryNameSpace;
using System.Reflection;

namespace DTDynMethod
{
    public class People
    {
        public string Name { get; set; }
        public uint Age { get; set; }
        public People() : this(string.Empty, 0)
        {
        }
        public People(string name, uint age)
        {
            Name = name;
            Age = age;
        }
        public string GetDetails()
        {
            return "Name is: " + Name + ". Age is: " + Age.ToString() + ".";
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
            Program p = new Program();
            var x = typeof(int?);
            var y = typeof(int);
            Console.WriteLine(p.Name);
            Console.WriteLine(p.URI.ToString());

            ConstructorInfo cinfo = typeof(People).GetConstructor(ctorFlags, null, Type.EmptyTypes, null);
            var ctor = DynamicMethodHandlerFactory<People, People, People>.CreateConstructor(cinfo);

            People ppp = ctor(typeof(People));

            ctor(ppp);

            Console.ReadLine();

        }

    }
}
