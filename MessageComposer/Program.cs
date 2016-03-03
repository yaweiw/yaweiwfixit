using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace MessageComposer
{
    class Program
    {
        static void Main(string[] args)
        {
            Message inputmessage = new Message
            {
                MessageId = new MessageId
                {
                    RepoId = new RepoId(new Guid(@"43fdea24-2538-3bc0-7c1e-539b16ad9e4e")),
                    BuildType = BuildType.Commit,
                    Action = BuildAction.Publish,
                    BuildId = new BuildId(@"201602020906316936-live")
                }
            };

            string data = JsonConvert.SerializeObject(inputmessage);

            Message outputmessage = JsonConvert.DeserializeObject<Message>(data);
        }
    }
}
