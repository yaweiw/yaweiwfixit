using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogitemProcessor
{
    public class LogitemMapper : MapperBase
    {
        public override void Map(string inputLine, MapperContext context)
        {
            if (inputLine.ToLowerInvariant().Equals("Verbose"))
                context.EmitLine(inputLine);
        }
    }
}
