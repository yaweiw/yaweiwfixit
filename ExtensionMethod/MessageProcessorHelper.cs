using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public static class MessageProcessorHelper
    {
        public static IMessageProcessor WithReport(this IMessageProcessor messageProcessor)
        {
            return new MessageProcessorWithReport(messageProcessor);
        }

        public static IMessageProcessor WithTimeOut(this IMessageProcessor messageProcessor, int messageProcessingTimeOutInSeconds)
        {
            return new MessageProcessorWithTimeOut(messageProcessor, messageProcessingTimeOutInSeconds);
        }

        public static IMessageProcessor WithSerializer(this IMessageProcessor messageProcessor)
        {
            return new MessageProcessorWithSerializer(messageProcessor);
        }
    }
}
