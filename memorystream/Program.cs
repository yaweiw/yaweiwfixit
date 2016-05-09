using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memorystream
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream ms = new MemoryStream();
            string str0 = @"================================================================";
            string str1 = @"Microsoft Advanced Threat Analytics (ATA) i s an on-premises product to help IT security professionals protect their enterprise from advanced targeted attacks by automatically analyzing, learning, and identifying normal and abnormal entity (user, devices, and resources) behavior. ATA also helps to identify known malicious attacks, security issues, and risks using world-class security researchers’ work regionally and globally. Leveraging user and entity behavioral analytics (UEBA), this innovative technology is designed to help enterprises focus on what is important and to identify security breaches before they cause damage.";
            string str2 = @"+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++";
            Console.WriteLine("Capacity = {0}, Length = {1}, Position = {2}\n",
                ms.Capacity.ToString(),
                ms.Length.ToString(),
                ms.Position.ToString());
            var writer = new StreamWriter(ms);
            for (int i = 0;i < 990000;i++)
            {
                writer.Write(str0);
                writer.Write(str1);
                writer.Write(str2);
            }
            Console.WriteLine("Capacity = {0}, Length = {1}, Position = {2}\n",
    ms.Capacity.ToString(),
    ms.Length.ToString(),
    ms.Position.ToString());
        }
    }
}
