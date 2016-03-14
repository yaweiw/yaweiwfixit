using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace DTDynMethod
{
    class Program
    {
        string Name => @"a program";
        Uri URI => new Uri(@"http://abab/");
        static void Main(string[] args)
        {
            Program p = new Program();
            var x = typeof(int?);
            var y = typeof(int);
            Console.WriteLine(p.Name);
            Console.WriteLine(p.URI.ToString());

            
        }

    }
}
