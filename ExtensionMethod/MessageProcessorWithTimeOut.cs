using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public class MessageProcessorWithTimeOut : MessageProcessBase
    {
        private readonly int _messageProcessingTimeOutInSeconds;

        public MessageProcessorWithTimeOut(IMessageProcessor inner, int messageProcessingTimeOutInSeconds)
            : base(inner)
        {
            _messageProcessingTimeOutInSeconds = messageProcessingTimeOutInSeconds;
        }

        public override async Task<string> ProcessAsync(string message)
        {
            await Task.Delay(500);
            return @"Message processed in MessageProcessorWithTimeOut";
        }
    }
}
