using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessageProcessor messageProcessor = new MessageProcessor();
            IMessageProcessor aggregatedmsgProcessor = messageProcessor.WithTimeOut(1000).WithReport().WithSerializer();
            Console.WriteLine(aggregatedmsgProcessor.GetType());
            Console.ReadLine();
        }
    }
}
