using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicHandleGeneratorsConsumer
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
            Random rnd = new Random();
            return "Name is: " + rnd.Next(365).ToString();// + Name + ". Age is: " + Age.ToString() + ".";
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
