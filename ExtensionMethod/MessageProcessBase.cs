using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public abstract class MessageProcessBase :IMessageProcessor
    {
        protected IMessageProcessor Inner { get; }

        protected MessageProcessBase(IMessageProcessor inner)
        {
            Inner = inner;
        }

        public virtual Task<string> ProcessAsync(string message)
        {
            return Inner.ProcessAsync(message);
        }
    }
}
