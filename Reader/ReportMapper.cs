using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mapper
{
    public static class ReportMapper
    {
        public static ReportModel map(string input, string pattern)
        {
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(input);
            StringBuilder htmlBuilder = new StringBuilder();
            // Report on the only match.
            GroupCollection groups = matches[0].Groups;
            htmlBuilder.Append(@"<tr Style=""background:#DEEAF6"">");
            htmlBuilder.Append(@"<td><a id="" &#xA;                      #@" + groups["sev"].Value + @"&#xA;                    ""> " + groups["sev"].Value + @" </a></td>""");
            htmlBuilder.Append(@"<td>" + groups["msg"].Value + @"</td>");
            htmlBuilder.Append(@"</td>" + groups["src"].Value + @"</td>");
            htmlBuilder.Append(@"<td></td>");
            htmlBuilder.Append(@"<td></td>");
            htmlBuilder.Append(@"<td>" + groups["dt"].Value + @"</td>");

            ReportModel rm = new ReportModel(groups["sev"].Value, htmlBuilder);
            return rm;
        }
    }
}
