﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reducer
{
    class Program
    {
        /*
      <tr Style="background:#DEEAF6">
        <td><a id="&#xA;                      #@Info&#xA;                    ">Info</a></td>
        <td>Plug-in directory: W:\5svjzm55.psb\source\.optemp\packages\docfx.msbuild.1.8.0-alpha-0015-g67b10bf\tools\plugins_f0jl5rzz.uim\plugins, configuration file: W:\5svjzm55.psb\source\.optemp\packages\docfx.msbuild.1.8.0-alpha-0015-g67b10bf\tools\plugins_f0jl5rzz.uim\plugins\docfx.plugins.config</td>
        <td>
        </td>
        <td><a href="https://github.com/openpublishtest/ope2etest-sandbox/blob/c79da1eb44852d84d4f1f90d7b237b35c78e82eb/#L"></a></td>
        <td>
        </td>
        <td>2016-04-10T14:57:46.9917325Z</td>
      </tr>
      */
        static void Main(string[] args)
        {
            string line;

            if (args.Length > 0)
            {
                Console.SetIn(new StreamReader(args[0]));
            }
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

            Console.WriteLine(header);
            Console.WriteLine(style);
            Console.WriteLine(headerend);
            Console.WriteLine(title);
            Console.WriteLine(tableheader);
            while ((line = Console.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(footer);
        }
    }
}
