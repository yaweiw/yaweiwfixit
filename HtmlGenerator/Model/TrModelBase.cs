using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator.Model
{
    public abstract class TrModelBase
    {
        protected string type;
        protected internal TrModelBase(string _type)
        {
            type = _type;
        }
    }
}
