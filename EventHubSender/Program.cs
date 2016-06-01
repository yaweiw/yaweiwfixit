namespace EventHub
{
    using System;

    using Microsoft.ServiceBus.Messaging;
    using EventHubSender;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus;
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Endpoint=sb://buildeventhub-ns.servicebus.windows.net/;SharedAccessKeyName=sendandlistenpolicy;SharedAccessKey=kngsABK9QKku9K+BOVPGxI1K6x26/z17nEhq+5Ua2qc=;EntityPath=buildeventhub";

            //string partitionId = @"c0d6fe70-95f3-ee98-7911-9eeeaa56d340";
            //Console.WriteLine("Press Ctrl-C to stop the sender process");
            //Console.WriteLine("Press Enter to start now");
            //Sender sender = new Sender(connectionString, partitionId);
            //Task<EventData> sendtask = sender.SendEventsAsync("a message");
            //sendtask.Wait();

            Receiver receiver = new Receiver(connectionString, "0");
            Task<string> t = receiver.ReceiveEventsAsync("0");
            t.Wait();
            Console.WriteLine(t.Result);
            Task<string> t1 = receiver.ReceiveEventsAsync("1");
            t1.Wait();
            Console.WriteLine(t1.Result);
            Task<string> t2 = receiver.ReceiveEventsAsync("2");
            t2.Wait();
            Console.WriteLine(t2.Result);
            Task<string> t3 = receiver.ReceiveEventsAsync("3");
            t3.Wait();
            Console.WriteLine(t3.Result);
        }
    }
}
