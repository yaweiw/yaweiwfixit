using Microsoft.ServiceBus.Messaging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHub
{
    public class Sender
    {
        private readonly string connectionString;
        private readonly string partitionId;

        public Sender(string constr, string partitionid)
        {
            connectionString = constr;
            partitionId = partitionid;
        }
        public async Task<EventData> SendEventsAsync(string repoId)
        {
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
            Console.WriteLine("Sending messages to Event Hub {0}", eventHubClient.Path);
            EventData buildEvent = new EventData(Encoding.UTF8.GetBytes($"repoId {DateTime.Now.ToString()}"));
            buildEvent.PartitionKey = "0";
            try
            {
                await eventHubClient.SendAsync(buildEvent);
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                Console.ResetColor();
            }
            return buildEvent;
        }
    }
}
