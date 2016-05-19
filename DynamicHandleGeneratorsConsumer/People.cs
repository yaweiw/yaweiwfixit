using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DynamicHandleGeneratorsConsumer
{
    public class People : IPeople
    {
        public string sth;
        private string name;
        private uint age;
        public string Name
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return name;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                name = value;
            }
        }
        public uint Age
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return age;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                age = value;
            }
        }
        public People() : this(string.Empty, 0)
        {
        }
        public People(string name, uint age)
        {
            sth = name + age;
            Name = name;
            Age = age;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetDetails()
        {
            Random rnd = new Random();
            return "Name: " + Name + ". Age is: " + Age.ToString() + rnd.Next(365).ToString();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetDetails(uint age)
        {
            Age = age;
            Random rnd = new Random();
            return "Age changed to: " + age.ToString() + rnd.Next(365).ToString();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetDetails(string name)
        {
            Name = name;
            Random rnd = new Random();
            return "Name changed to: " + name + rnd.Next(365).ToString();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Calculate(int x)
        {
            Random rnd = new Random();
            x = x * rnd.Next(1000);
            return x * 2;
        }

    }
}
