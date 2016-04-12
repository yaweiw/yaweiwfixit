using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mapper
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
        enum SevLevel { Error, Warning, Info, Verbose };
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.SetIn(new StreamReader(args[0]));
            }
            string line;
            /* {"message\W+(?<msg>.+)\W+source\W+(?<src>\w+).+message_severity.+(?<sev>[0-3]{1}|[EWIV]{1}[rane]{1}[rf]{1}[nob]{1}[rio]?[ns]?[ge]?)\W+date_time\W+(?<dt>.+)"} */
            string pattern = @"{""message\W+(?<msg>.+)""\W+source\W+(?<src>\w+).+message_severity.+(?<sev>[0-3]{1}|[EWIV]{1}[rane]{1}[rf]{1}[nob]{1}[rio]?[ns]?[ge]?)\W+date_time\W+(?<dt>.+)""}";
            while ((line = Console.ReadLine()) != null)
            {
                Console.WriteLine(ReportMapper.map(line, pattern).ToString());
            }
        }
    }
}
