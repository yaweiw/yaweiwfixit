using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator.Model
{
    public class BuildDetailsTrModel : TrModelBase
    {
        public string Severity { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string File { get; set; }
        public string Line { get; set; }
        public string Time { get; set; }
        public BuildDetailsTrModel():this(null,null,null,null,null,null)
        { }

        public BuildDetailsTrModel(
            string severity,
            string message,
            string source,
            string file,
            string line,
            string time):base("BuildDetailsTrModel")
        {
            Severity = severity;
            Message = message;
            Source = source;
            File = file;
            Line = line;
            Time = time;
        }
    }
}
