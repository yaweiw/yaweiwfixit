using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public class MessageProcessorWithSerializer : MessageProcessBase
    {
        private readonly object _someotherobj;

        public MessageProcessorWithSerializer(IMessageProcessor inner)
            : this(inner, @"MessageProcessorWithSerializer string object")
        {
        }

        internal MessageProcessorWithSerializer(IMessageProcessor inner, object someotherobj)
            : base(inner)
        {
            _someotherobj = someotherobj;
        }

        public override async Task<string> ProcessAsync(string message)
        {
            await Task.Delay(500);
            return @"Message processed in MessageProcessorWithSerializer";
        }
    }
}
