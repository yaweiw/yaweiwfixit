using Mapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(@"C:\Work\l.txt");
            /* {"message\W+"(?<msg>.+)"\W+source\W+"(?<src>.+)"\W+file\W+:(?<file>.+),\W+line\W+:(?<line>.+),\W+message_severity.+(?<sev>[0-3]{1}|[EWIV]{1}[rane]{1}[rf]{1}[nob]{1}[rio]?[ns]?[ge]?)\W+date_time\W+(?<dt>.+)"} */
            string pattern = @"{""message\W+""(?<msg>.+)""\W+source\W+""(?<src>.+)""\W+file\W+:(?<file>.+),\W+line\W+:(?<line>.+),\W+message_severity.+(?<sev>[0-3]{1}|[EWIV]{1}[rane]{1}[rf]{1}[nob]{1}[rio]?[ns]?[ge]?)\W+date_time\W+(?<dt>.+)""}";
            string header = @"<html><head><META http-equiv=""Content - Type"" content=""text / html; charset = utf - 8""><meta http-equiv=""Content - Type"" content=""text / html; charset = windows - 1252"">";
            string style = @"    <style>
          /* Font Definitions */
          @font-face {
          font-family: SimSun;
          panose-1: 2 1 6 0 3 1 1 1 1 1;
          mso-font-alt: SimSun;
          mso-font-charset: 134;
          mso-generic-font-family: auto;
          mso-font-pitch: variable;
          mso-font-signature: 3 680460288 22 0 262145 0;
          }
          @font-face {
          font-family: ""Cambria Math"";
          panose - 1: 2 4 5 3 5 4 6 3 2 4;
            mso - font - alt: ""Calisto MT"";
            mso - font - charset: 0;
            mso - generic - font - family: roman;
            mso - font - pitch: variable;
            mso - font - signature: -536870145 1107305727 0 0 415 0;
        }
        @font-face {
          font-family: Calibri;
          panose-1: 2 15 5 2 2 2 4 3 2 4;
          mso-font-alt: ""Times New Roman"";
          mso-font-charset: 0;
          mso-generic-font-family: swiss;
          mso-font-pitch: variable;
          mso-font-signature: -520092929 1073786111 9 0 415 0;
          }

    @font-face {
          font-family: ""\@SimSun"";
          panose-1: 2 1 6 0 3 1 1 1 1 1;
          mso-font-alt: ""\@Arial Unicode MS"";
          mso-font-charset: 134;
          mso-generic-font-family: auto;
          mso-font-pitch: variable;
          mso-font-signature: 3 680460288 22 0 262145 0;
          }
/* Style Definitions */
p.MsoNormal, li.MsoNormal, div.MsoNormal {
          mso-style-unhide: no;
          mso-style-qformat: yes;
          mso-style-parent: "";
          margin: 0in;
          margin-bottom: .0001pt;
          mso-pagination: widow-orphan;
          font-size: 11.0pt;
          font-family: ""Calibri"",""sans -serif"";
          mso-fareast-font-family: SimSun;
          mso-fareast-theme-font: minor-fareast;
          }

          a:link, span.MsoHyperlink {
          mso-style-noshow: yes;
          mso-style-priority: 99;
          color: #0563C1;
          text-decoration: underline;
          text-underline: single;
          }

          a:visited, span.MsoHyperlinkFollowed {
          mso-style-noshow: yes;
          mso-style-priority: 99;
          color: #954F72;
          text-decoration: underline;
          text-underline: single;
          }

          span.EmailStyle17 {
          mso-style-type: personal;
          mso-style-noshow: yes;
          mso-style-unhide: no;
          font-family: ""Calibri"",""sans -serif"";
          mso-ascii-font-family: Calibri;
          mso-hansi-font-family: Calibri;
          mso-bidi-font-family: Calibri;
          color: windowtext;
          }

          .MsoChpDefault {
          mso-style-type: export-only;
          mso-default-props: yes;
          font-size: 10.0pt;
          mso-ansi-font-size: 10.0pt;
          mso-bidi-font-size: 10.0pt;
          }

          @page WordSection1
{
    size: 8.5in 11.0in;
    margin: 1.0in 1.0in 1.0in 1.0in;
    mso-header-margin: .5in;
    mso-footer-margin: .5in;
    mso-paper-source: 0;
}

div.WordSection1 {
          page: WordSection1;
          }

          td {
          font-family: ""Calibri"",""sans -serif"";
          font-size: 11.0pt;
          border: solid #5B9BD5 1.0pt;
          padding: 1pt 5.4pt 1pt 5.4pt;
          }

          pre {
          margin-bottom: .0001pt;
          font-size: 10.0pt;
          font-family: ""Verdana"", sans-serif;
          margin-left: 0in;
          margin-right: 0in;
          margin-top: 0in;
          }

          .auto-style1 {
          width: 90.0%;
          border-collapse: collapse;
          font-size: 10.0pt;
          font-family: ""Times New Roman"", serif;
          }
        </style>";
            string headerend = @" </head>";
            string title = @"<body lang=""EN - US"" link=""#0563C1"" vlink=""#954F72"" style=""tab-interval:.5in""><p class=""MsoNormal""><span style=""font-size:16pt""><b>Open Publishing Online Build Report</b></span></p>";
            //string tablecoldef = @"<table class=""MsoNormalTable"" border=""0"" style=""border - collapse:collapse; mso - yfti - tbllook:1184; mso - padding - alt:0in 0in 0in 0in"">`
            //                       <tr style=""mso - yfti - irow: 0; mso - yfti - firstrow: yes; background: #5B9BD5"">`
            //                       <td><p class=""MsoNormal""><b><span style=""color: white"">File Name</span></b></p></td>`
            //                       <td><p class=""MsoNormal""><b><span style=""color: white"">Error</span></b></p></td>`
            //                       <td><p class=""MsoNormal""><b><span style=""color: white"">Warning</span></b></p></td>`
            //                       <td><p class=""MsoNormal""><b><span style=""color: white"">Info</span></b></p></td></tr>";
            string tableheader = @"</table><br><p class=""MsoNormal""><span style=""font - size:14pt""><b>Build Details</b></span></p>
                                   <table class=""MsoNormalTable"" border=""0"" style=""border-collapse:collapse;mso-yfti-tbllook:1184;mso-padding-alt:0in 0in 0in 0in"">
                                   <tr style = ""mso-yfti-irow: 0; mso-yfti-firstrow: yes; background: #5B9BD5""><td>
                                   <p class=""MsoNormal""><b><span style = ""color: white""> Severity </span></b></p></td>
                                   <td><p class=""MsoNormal""><b><span style = ""color: white""> Message </span></b></p></td>
                                   <td><p class=""MsoNormal""><b><span style = ""color: white""> Source </span></b></p></td>
                                   <td><p class=""MsoNormal""><b><span style = ""color: white""> File </span></b></p></td>
                                   <td><p class=""MsoNormal""><b><span style = ""color: white"" > Line </span></b></p></td>
                                   <td><p class=""MsoNormal""><b><span style = ""color: white"" > Time </span></b></p></td></tr>";
            string footer = @"</table><br><a href="">Build Raw Log</a></body></html>";
            Stopwatch st = new Stopwatch();
            st.Start();
            Dictionary<string, string> sev_line = new Dictionary<string, string>();

            while ((line = file.ReadLine()) != null)
            {
                //key is msg, value is msg_sev
                var ret = ReportMapper.map(line, pattern);
                if(!sev_line.ContainsKey(ret.Key)) sev_line.Add(ret.Key, ret.Value);
            }
            Stopwatch st1 = new Stopwatch();
            st1.Start();
            //Sort By descending
            var query =
                from entry in sev_line
                group entry by entry.Value into newGroup
                orderby newGroup.Key[newGroup.Key.Length-1] descending
                select newGroup;
            st1.Stop();
            using (System.IO.StreamWriter output = new System.IO.StreamWriter(@"C:\Work\output.html"))
            {
                output.WriteLine(header);
                output.WriteLine(style);
                output.WriteLine(headerend);
                output.WriteLine(title);
                output.WriteLine(tableheader);
                foreach(var item in query)
                {
                    foreach(var pair in item)
                    output.WriteLine(pair.Key);
                }
                output.WriteLine(footer);
            }
            file.Close();
            st.Stop();
            TimeSpan ts = st.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            TimeSpan ts1 = st1.Elapsed;
            string elapsedTime1 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts1.Hours, ts1.Minutes, ts1.Seconds,
    ts1.Milliseconds / 10);
            // Suspend the screen.
            Console.WriteLine("Done! {0}:{1}", elapsedTime,elapsedTime1);
            Console.ReadLine();

        }
    }
}
