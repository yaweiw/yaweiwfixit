using System.Xml.Xsl;
using System.Diagnostics;
using System;

namespace Xsltransform
{
    class Program
    {
        static XslCompiledTransform staticXslt = new XslCompiledTransform();
        static void init()
        {
            staticXslt.Load(@"C:\Work\tests\yaweiwfixit\Xsltransform\output.xsl");
        }
        static void transform()
        {
            // Load the style sheet.
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"C:\Work\tests\yaweiwfixit\Xsltransform\output.xsl");

            // Execute the transform and output the results to a file.
            xslt.Transform(@"C:\Work\tests\yaweiwfixit\Xsltransform\books.xml", @"C:\Work\tests\yaweiwfixit\Xsltransform\books.html");
        }

        static void staticTransform()
        {
            staticXslt.Transform(@"C:\Work\tests\yaweiwfixit\Xsltransform\books.xml", @"C:\Work\tests\yaweiwfixit\Xsltransform\books.html");
        }
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for(int i = 0; i<1000; i++)
            {
                transform();
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("transform RunTime " + elapsedTime);

            init();
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            for (int i = 0; i < 1000; i++)
            {
                staticTransform();
            }
            stopWatch2.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts2 = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts2.Hours, ts2.Minutes, ts2.Seconds,
                ts2.Milliseconds / 10);
            Console.WriteLine("staticTransform RunTime " + elapsedTime2);
            Console.ReadLine();
        }
    }
}
