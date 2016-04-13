using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator.Model
{
    public class BuildDetailsTrEntry : TrModelBase
    {
        public string Severity { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string File { get; set; }
        public string Line { get; set; }
        public string Time { get; set; }

        public BuildDetailsTrEntry():this(null,null,null,null,null,null)
        { }

        public BuildDetailsTrEntry(string severity,
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
