using HtmlGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public class BuildDetailsHtmlPlaceHolder : ContentPlaceHolderBase
    {
        public string BuildDetailsHead
        {
            get { return head; }
        }
        public BuildDetailsHtmlPlaceHolder()
            : this(null,null)
        {
        }

        // head: <P class="MsoNormal"><SPAN style="font-size: 14pt;"><B>Build Details</B></SPAN></P>
        public BuildDetailsHtmlPlaceHolder(string _head, List<TrModelBase> _tablecontent):base(_head, _tablecontent.ConvertAll<object>(x => (object)x), string.Empty)
        {
        }
        /*
        $severity: <A id="&#10;#@Info&#10;">Info</A>
        $message: string
        $source: string
        $file: <A href="https://github.com/Microsoft/win-cpub-itpro-docs/blob/e074ca48d307e14e4590666cee8f6b116d15319a/#L">??</A>
        $line: string
        $time: 2016-04-12T23:16:27.3196693Z
        */
        public override StringBuilder HtmlTableGenerator()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(head);
            builder.Append("<TABLE class=\"MsoNormalTable\" style=\"border-collapse: collapse; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in;\" border=\"0\">");
            builder.Append("<TBODY>");
            builder.Append("<TR style=\"background: rgb(91, 155, 213); mso-yfti-irow: 0; mso-yfti-firstrow: yes;\">");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Severity</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Message</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Source</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">File</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Line</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Time</SPAN></B></P></TD></TR>");
            // TdCont: <A href=""https://xxx"">xxx</A>
            // $background: style=""background: rgb(222, 234, 246); ""
            bool background = true;
            string trpattern = @"<TR $background><TD>$severity</TD><TD>$message</TD><TD>$source</TD><TD>$file</TD><TD>$line</TD><TD>$time</TD></TR>";
            string backgroundcolor = @"style=""background: rgb(222, 234, 246); """;
            foreach (BuildDetailsTrModel entry in content)
            {
                if (background)
                {
                    builder.Append(
                    trpattern.Replace("$background", backgroundcolor)
                    .Replace("severity", entry.Severity)
                    .Replace("$message", entry.Message)
                    .Replace("$source", entry.Source)
                    .Replace("$file", entry.File)
                    .Replace("$line", entry.Line)
                    .Replace("$time", entry.Time));
                    background = false;
                }
                else
                {
                    builder.Append(
                    trpattern.Replace("severity", entry.Severity)
                    .Replace("$message", entry.Message)
                    .Replace("$source", entry.Source)
                    .Replace("$file", entry.File)
                    .Replace("$line", entry.Line)
                    .Replace("$time", entry.Time));
                    background = true;
                }
                
            }
            builder.Append("  </TBODY></TABLE><BR>");
            builder.Append(foot);
            return builder;
        }
    }
}
