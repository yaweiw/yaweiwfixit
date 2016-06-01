using Microsoft.ServiceBus.Messaging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSender
{
    public class Receiver
    {
        private readonly string connectionString;
        public string startingOffset;

        public Receiver(string constr, string offset)
        {
            connectionString = constr;
            startingOffset = offset;
        }
        public async Task<string> ReceiveEventsAsync(string partitionId, long receiverEpoch = 0)
        {
            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(connectionString);
            EventHubConsumerGroup consumerGroup = eventHubClient.GetDefaultConsumerGroup();
            EventHubReceiver consumer;

            if (startingOffset != null)
            {
                consumer = await consumerGroup.CreateReceiverAsync(partitionId, startingOffset, receiverEpoch); // All messages
            }
            else
            {
                // Default to get oldest message
                consumer = await consumerGroup.CreateReceiverAsync(partitionId, receiverEpoch); // All messages
            }
            string msg = null;
            try
            {
                var message = await consumer.ReceiveAsync();
                do
                {
                    if (message != null)
                    {
                        var info = message.GetBytes();
                        msg = Encoding.UTF8.GetString(info);

                        Console.WriteLine("Processing: Seq number {0} Offset {1}  Partition {2} EnqueueTimeUtc {3} Message {4}",
                            message.SequenceNumber, message.Offset, message.PartitionKey, message.EnqueuedTimeUtc.ToShortTimeString(), msg);
                    }
                    message = await consumer.ReceiveAsync();
                }
                while (message != null);
            }
            catch (Exception exception)
            {
                Console.WriteLine("exception on receive {0}", exception.Message);
            }
            return msg;
        }
    }
}
