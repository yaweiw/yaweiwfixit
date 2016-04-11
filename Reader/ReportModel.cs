using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class ReportModel
    {
        public string Severity { get; }
        public StringBuilder htmlBuilder { get; }
        public ReportModel(string sev, StringBuilder builder)
        {
            Severity = sev;
            htmlBuilder = builder;
        }
    }
}
