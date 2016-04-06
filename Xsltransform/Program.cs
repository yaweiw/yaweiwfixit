using System.Xml.Xsl;
using System.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Microsoft.OpenPublishing.Build.Loggers.Interface;
using Microsoft.OpenPublishing.Build.Common;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;

namespace Xsltransform
{
    public class OnlineBuildReport
    {
        [XmlIgnore]
        public string ReportName => "OnlineBuildReport";

        [XmlIgnore]
        public string XslTransformFile => "OnlineBuildReport.xsl";

        [XmlIgnore]
        public bool ReferenceLocalStylesheet => false;

        public string GitRepositoryUrl { get; set; }

        public string TriggeredBy { get; set; }

        public MessageSeverity ReportSeverityLevel { get; set; }

        public string BuildRawLogUrl { get; set; }

        [XmlArrayItem("SourceFile")]
        public string[] BuildFiles { get; set; }

        [XmlArrayItem("LogItem")]
        public LogItem[] BuildLogItems { get; set; }
    }

    class Program
    {
        static string stylesheetUri = @"C:\Work\tests\yaweiwfixit\Xsltransform\ReportTransform\OnlineBuildReport.xsl";
        static string logFilePath = @"C:\Work\tests\yaweiwfixit\Xsltransform\ReportTransform\workflow_report.txt";
        static string resultsFile = @"C:\Work\tests\yaweiwfixit\Xsltransform\ReportTransform\output.html";
        static ConcurrentDictionary<string, XslCompiledTransform> _xslTransforms = new ConcurrentDictionary<string, XslCompiledTransform>();

        static XslCompiledTransform GetXslTransform()
        {
            return _xslTransforms.GetOrAdd(
                "OnlineBuildReport",
                name =>
                {
                    var transform = new XslCompiledTransform();
                    transform.Load(stylesheetUri);
                    return transform;
                });
        }

        static void Transform(OnlineBuildReport report, Stream result)
        {
            var serializer = new XmlSerializer(report.GetType());

            using (Stream xmlStream = new MemoryStream())
            {
                serializer.Serialize(xmlStream, report);
                xmlStream.Position = 0;

                using (XmlReader reader = new XmlTextReader(xmlStream))
                {
                    XslCompiledTransform xslTransform = GetXslTransform();
                    xslTransform.Transform(reader, null, result);
                    result.Flush();
                    result.Position = 0;
                }
            }
        }

        private static IEnumerable<LogItem> LoadLogItems(string logFilePath)
        {
            IEnumerable<string> lines;

            try
            {
                lines = File.ReadLines(logFilePath);
            }
            catch (FileNotFoundException)
            {
                yield break;
            }

            foreach (var line in lines)
            {
                LogItem logItem = null;
                try
                {
                    logItem = JsonUtility.FromJsonString<LogItem>(line);
                }
                catch (ArgumentException)
                {
                }

                if (logItem != null)
                {
                    yield return logItem;
                }
            }
        }

        static void Main(string[] args)
        {

            Stopwatch stopWatch1 = new Stopwatch();
            stopWatch1.Start();
            Console.WriteLine("Part 1 starts...");

            LogItem[] buildLogItems =
                (from logItem in LoadLogItems(logFilePath)
                 where logItem != null && logItem.MessageSeverity <= MessageSeverity.Verbose
                 select logItem).ToArray();

            stopWatch1.Stop();
            Console.WriteLine("Part 1 stops...");
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts1 = stopWatch1.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts1.Hours, ts1.Minutes, ts1.Seconds,
                ts1.Milliseconds / 10);
            Console.WriteLine("Part 1 RunTime " + elapsedTime1);

            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            Console.WriteLine("Part 2 starts...");

            OnlineBuildReport report = new OnlineBuildReport()
            {
                BuildFiles = null,
                //BuildFinishedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                BuildLogItems = buildLogItems,
                BuildRawLogUrl = @"http://127.0.0.1:10000/devstoreaccount1/report/2016/4/6/4b7ba5e9-eefe-54da-c39f-4e52ae79fa8c/Commit/201602020906316936-live/rawlog.txt",
                //BuildTime = "00:08:02",
                //BuildType = "Commit",
                //GitBranchName = "live",
                //GitCommitId = "150c06a3decc7c8b95d1d9d51b9f03a07ca9a47f",
                TriggeredBy = null,
                //GitRepositoryName = "Azure-Content",
                GitRepositoryUrl = "https://github.com/Azure-Content",
                //RepoType = "Github",
                ReportSeverityLevel = MessageSeverity.Verbose
            };

            Stream result = new MemoryStream();
            Transform(report, result);
            FileStream fileStream = File.Create(resultsFile, (int)result.Length);
            // Initialize the bytes array with the stream length and then fill it with data
            byte[] bytesInStream = new byte[result.Length];
            result.Read(bytesInStream, 0, bytesInStream.Length);
            // Use write method to write to the file specified above
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);

            stopWatch2.Stop();
            Console.WriteLine("Part 2 stops...");
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts2 = stopWatch2.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts2.Hours, ts2.Minutes, ts2.Seconds,
                ts2.Milliseconds / 10);
            Console.WriteLine("Part 2 RunTime " + elapsedTime2);
            Console.ReadLine();
        }
    }
}
