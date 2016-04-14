using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator.Model
{
    public class BuildSummaryTrModel : TrModelBase
    {
        public string TdCont { get; set; }
        public BuildSummaryTrModel(string tdcont) : base("BuildSummaryTrModel")
        {
            TdCont = tdcont;
        }
    }
}
