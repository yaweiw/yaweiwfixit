using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator.Model
{
    public class BuildFilesTrEntry : TrModelBase
    {
        public string FileName { get; set; }
        public string Error { get; set; }
        public string Warning { get; set; }
        public string Info { get; set; }
        public BuildFilesTrEntry():this(null,null,null,null)
        { }

        public BuildFilesTrEntry(string severity,
            string message,
            string source,
            string file,
            string line,
            string time):base("BuildDetailsTrEntry")
        {
            Severity = message;
            Source = source;
            File = file;
            Line = line;
            Time = time;
        }
    }
}
