using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public interface IHtmlHolder
    {
        StringBuilder HtmlTableGenerator();
    }
}