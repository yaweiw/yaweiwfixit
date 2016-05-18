using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicHandleGeneratorsTests
{
    public class TestHelper
    {
        public string Prop1 { get; set; }
        public string Prop2 { get; private set; }
        public TestHelper(string prop1, string prop2)
        {
            Prop1 = prop1;
            Prop2 = prop2;
        }
        public string GetAllPropsAsString()
        {
            return Prop1 + Prop2;
        }
    }
}
