using HtmlGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGeneratorCS
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportGenerator rg = new ReportGenerator();
            using (System.IO.StreamWriter output = new System.IO.StreamWriter(@"C:\Work\output.html"))
            {
                output.WriteLine(rg.HtmlHeaderBeginTag.ToString());
                output.WriteLine(rg.HtmlBodyBeginTag.ToString());
                output.WriteLine(rg.HtmlReportHeader.ToString());
                output.WriteLine(rg.HtmlBuildSummary.ToString());
                output.WriteLine(rg.HtmlBuildFiles.ToString());
                output.WriteLine(rg.HtmlBuildDetails.ToString());
                output.WriteLine(rg.HtmlBodyEndTag.ToString());
                output.WriteLine(rg.HtmlRawLog.ToString());
                output.WriteLine(rg.HtmlHeaderEndTag.ToString());
            }
        }
    }
}
