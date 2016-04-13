using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public static class HtmlGeneratorHelper
    {
        // <TD><P class="MsoNormal"><SPAN>$Col</SPAN></P></TD>
        public static string HtmlColGenerator(string colcontent, string colpattern)
        {
            return colpattern.Replace("$Col", colcontent);
        }

        // <TD><P class="MsoNormal"><A href="https://github.com/Microsoft/win-cpub-itpro-docs/commit/e074ca48d307e14e4590666cee8f6b116d15319a">e074ca48d307e14e4590666cee8f6b116d15319a</A></P></TD></TR>
        public static string HtmlColGenerator(string colcontent1, string colcontent2, string colpattern)
        {
            return colpattern.Replace("$Col1", colcontent1).Replace("$Col2", colcontent2);
        }

        /*
        <TR style="mso-yfti-irow: 0; mso-yfti-firstrow: yes;">
    <TD><P class="MsoNormal"><SPAN>Triggered By:</SPAN></P></TD>
    <TD><P class="MsoNormal"><A href="mailto:heatherpoulsen@users.noreply.github.com">heatherpoulsen@users.noreply.github.com</A></P></TD>
    </TR>
    */
        public static string HtmlRowGenerator(string col1, string col2, string rowpattern)
        {
            return rowpattern.Replace("$col1", rowpattern).Replace("$col2", rowpattern);
        }
    }
}
