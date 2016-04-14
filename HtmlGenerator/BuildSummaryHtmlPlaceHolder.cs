using HtmlGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public class BuildSummaryHtmlPlaceHolder : ContentPlaceHolderBase
    {
        public BuildSummaryHtmlPlaceHolder()
            : this(null,null)
        {
        }
        public BuildSummaryHtmlPlaceHolder(string _head, List<TrModelBase> _tablecontent):base(_head,_tablecontent.ConvertAll<object>(x=>(object)x),string.Empty)
        {
        }
        public override StringBuilder HtmlTableGenerator()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(head);
            builder.Append("<TABLE class=\"MsoNormalTable\" style=\"border-collapse: collapse; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in;\" border=\"0\">");
            builder.Append("<TBODY>");

            // TdCont: <A href=""https://xxx"">xxx</A>
            string trpattern = @"<TR style=""mso-yfti-irow: 0; mso-yfti-firstrow: yes;""><TD><P class=""MsoNormal""><SPAN>GitHub Repository (Branch):</SPAN></P></TD><TD><P class=""MsoNormal""><SPAN>$tdcont</SPAN></P></TD></TR>";
            foreach (BuildSummaryTrModel entry in content)
            {
                builder.Append(
                    trpattern.Replace("$tdcont", entry.TdCont)
                    );
            }
            builder.Append("</TBODY></TABLE><BR>");
            builder.Append(foot);
            return builder;
        }
    }
}
