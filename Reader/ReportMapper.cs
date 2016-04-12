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
        public static KeyValuePair<string,string> map(string input, string pattern)
        {
            try
            {
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(input);
                StringBuilder htmlBuilder = new StringBuilder();
                // Report on the only match.
                GroupCollection groups = matches[0].Groups;
                htmlBuilder.Append(@"<tr>");
                htmlBuilder.Append(@"<td>" + groups["sev"].Value + @"</td>");
                htmlBuilder.Append(@"<td>" + groups["msg"].Value + @"</td>");
                htmlBuilder.Append(@"<td>" + groups["src"].Value + @"</td>");
                htmlBuilder.Append(@"<td></td>");
                htmlBuilder.Append(@"<td></td>");
                htmlBuilder.Append(@"<td>" + groups["dt"].Value + @"</td>");
                htmlBuilder.Append(@"</tr>");
                return new KeyValuePair<string, string>(groups["sev"].Value, htmlBuilder.ToString());
            }
            catch (Exception e)
            {
                return new KeyValuePair<string, string>(e.Message, e.StackTrace);
            }
        }
    }
}
