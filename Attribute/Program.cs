using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTest
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PartitionAttribute: Attribute
    {
        public string PartitionKey { get; }
        public PartitionAttribute(string partitionkey)
        {
            PartitionKey = partitionkey;
        }

    }

    [Partition("201602020906316936-live")]
    public class Partition
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Partition p = new Partition();
            var attr = (PartitionAttribute)Attribute.GetCustomAttribute(typeof(Partition), typeof(PartitionAttribute), true);
            Console.WriteLine(attr.PartitionKey);
        }
    }
}
