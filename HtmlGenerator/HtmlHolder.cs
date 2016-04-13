using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public abstract class HtmlHolder : IHtmlHolder
    {
        protected readonly string head;
        protected readonly List<KeyValuePair<string, string>> tablecontent;

        public abstract StringBuilder HtmlTableGenerator();
        protected internal HtmlHolder(string _head, List<KeyValuePair<string,string>> _tablecontent)
        {
            head = _head;
            tablecontent = _tablecontent;
        }
    }
}
