using HtmlGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    /// <summary>
    /// Content place holder
    /// </summary>
    public abstract class ContentPlaceHolderBase : IContentPlaceHolder
    {
        protected readonly string head;
        protected readonly List<object> content;
        protected readonly string foot;

        /// <summary>
        /// generates Html table markup
        /// </summary>
        /// <returns></returns>
        public abstract StringBuilder HtmlTableGenerator();
        protected internal ContentPlaceHolderBase(string _head, List<object> _content, string _foot)
        {
            head = _head;
            content = _content;
            foot = _foot;
        }
    }
}
