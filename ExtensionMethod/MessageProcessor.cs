using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethod
{
    public class MessageProcessor : IMessageProcessor
    {
        public async Task<string> ProcessAsync(string message)
        {
            await Task.Delay(500);
            return @"Message processed in MessageProcessor";
        }
    }
}
