using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Tests
{
    [TestClass()]
    public class ReportMapperTests
    {
        [TestMethod()]
        public void mapTest()
        {
            // arrange
            string input = @"{""message"":""No provisioned docset found in repository 4b7ba5e9 - eefe - 54da - c39f - 4e52ae79fa8c.Please provision or restore the docsets if you want to build them"",""source"":""LoadPublishConfig"",""file"":null,""line"":null,""message_severity"":1,""date_time"":""2016 - 04 - 05T05: 40:19.1854244Z""}";
            string pattern = @"{""message\W+(?<msg>.+)""\W+source\W+(?<src>\w+).+message_severity.+(?<sev>[0-3]{1})\W+date_time\W+(?<dt>.+)""}";
            // act
            //ReportModel model = ReportMapper.map(input, pattern);
            // assert
            Assert.Fail();
        }
    }
}