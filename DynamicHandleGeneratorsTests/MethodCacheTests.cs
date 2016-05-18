using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicMethodHandleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicHandleGeneratorsTests;

namespace DynamicMethodHandleGenerators.Tests
{
    [TestClass()]
    public class MethodCacheTests
    {
        [TestMethod()]
        public void GetCachedMethodHandleByCacheKeyTest()
        {
            // arrange
            TestHelper testHelper = new TestHelper("prop1", "prop2");
            // act
            DynamicMethodHandle dmh = MethodCache.GetCachedMethodHandleByCacheKey(testHelper, "GetAllPropsAsString");
            var result = dmh.DynamicMethod(testHelper, null);
            // assert
            Assert.IsNotNull(dmh);
            Assert.IsNotNull(dmh.DynamicMethod);
        }
    }
}