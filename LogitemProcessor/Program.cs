using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogitemProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            HadoopJobConfiguration hadoopConfiguration = new HadoopJobConfiguration();
            hadoopConfiguration.InputPath = "/input";
            hadoopConfiguration.OutputFolder = "/output";
            Uri myUri = new Uri("DEV URL for Hadoop");
            IHadoop hadoop = Hadoop.Connect(myUri, "user_name", "pwn");

            hadoop.MapReduceJob.Execute<LogitemMapper, LogitemReducer>(hadoopConfiguration);

            Console.Read();
        }
    }
}
