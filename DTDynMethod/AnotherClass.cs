using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
