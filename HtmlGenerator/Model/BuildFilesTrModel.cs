using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator.Model
{
    public class BuildFilesTrModel : TrModelBase
    {
        public string FileName { get; set; }
        public string Error { get; set; }
        public string Warning { get; set; }
        public string Info { get; set; }
        public BuildFilesTrModel():this(null,null,null,null)
        { }

        public BuildFilesTrModel(
            string filename,
            string error,
            string warning,
            string info):base("BuildFilesTrModel")
        {
            FileName = filename;
            Error = error;
            Warning = warning;
            Info = info;
        }
    }
}
