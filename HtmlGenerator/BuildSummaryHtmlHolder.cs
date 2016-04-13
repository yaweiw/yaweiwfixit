using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public class BuildSummaryHtmlHolder : HtmlHolder
    {
        public string BuildSummaryHead
        {
            get { return head; }
        }
        public BuildSummaryHtmlHolder()
            : this(null,null)
        {
        }
        public BuildSummaryHtmlHolder(string _head, List<BuildSummaryTrEntry> _tablecontent):base(_head,_tablecontent)
        {
        }
        public StringBuilder HtmlTableGenerator()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<TABLE class=\"MsoNormalTable\" style=\"border-collapse: collapse; mso-yfti-tbllook: 1184; mso-padding-alt: 0in 0in 0in 0in;\" border=\"0\">");
            builder.Append("  <TBODY>");

            string trpattern = @"<TR style=""mso-yfti-irow: 0; mso-yfti-firstrow: yes;""><TD><P class=""MsoNormal""><SPAN>GitHub Repository (Branch):</SPAN></P></TD>$Col</TR>"
            string tdpattern = @"<TD><P class=""MsoNormal""><SPAN><A href=""$col1"">$col2</A></SPAN></P></TD>";
            foreach (var entry in tablecontent)
            {
                HtmlGeneratorHelper.HtmlRowGenerator(HtmlGeneratorHelper.HtmlColGenerator());
            }
            builder.Append("  </TBODY></TABLE><BR>");
            return builder;
        }
    }
}
