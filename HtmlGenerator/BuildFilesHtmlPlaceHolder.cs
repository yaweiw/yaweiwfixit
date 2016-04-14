using HtmlGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public class BuildFilesHtmlPlaceHolder : ContentPlaceHolderBase
    {
        public string BuildFilesHead
        {
            get { return head; }
        }
        public BuildFilesHtmlPlaceHolder()
            : this(null,null)
        {
        }
        public BuildFilesHtmlPlaceHolder(string _head, List<TrModelBase> _tablecontent):base(_head, _tablecontent.ConvertAll<object>(x => (object)x), string.Empty)
        {
        }
        public override StringBuilder HtmlTableGenerator()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(head);
            builder.Append("<TABLE class=\"MsoNormalTable\" style=\"border-collapse: collapse; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in;\" border=\"0\">");
            builder.Append("<TBODY>");
            builder.Append("<TR style=\"background: rgb(91, 155, 213); mso-yfti-irow: 0; mso-yfti-firstrow: yes;\">");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">File Name</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Error</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Warning</SPAN></B></P></TD>");
            builder.Append("<TD>");
            builder.Append("<P class=\"MsoNormal\"><B><SPAN style=\"color: white;\">Info</SPAN></B></P></TD></TR>");

            // TdCont: <A href=""https://xxx"">xxx</A>
            // $background: style=""background: rgb(222, 234, 246); ""
            bool background = true;
            string trpattern = @"<TR $background><TD>$filename</TD><TD align=""right"">$error</TD><TD align=""right"">$warning</TD><TD align=""right"">$info</TD></TR>";
            string backgroundcolor = @"style=""background: rgb(222, 234, 246); """;
            foreach (BuildFilesTrModel entry in content)
            {
                if (background)
                {
                    builder.Append(
                    trpattern.Replace("$background", backgroundcolor)
                    .Replace("$filename", entry.FileName)
                    .Replace("$error", entry.Error)
                    .Replace("$warning", entry.Warning)
                    .Replace("$info", entry.Info));
                    background = false;
                }
                else
                {
                    builder.Append(
                    trpattern.Replace("$filename", entry.FileName)
                    .Replace("$error", entry.Error)
                    .Replace("$warning", entry.Warning)
                    .Replace("$info", entry.Info));
                    background = true;
                }
            }
            builder.Append("</TBODY></TABLE><BR>");
            builder.Append(foot);
            return builder;
        }
    }
}
