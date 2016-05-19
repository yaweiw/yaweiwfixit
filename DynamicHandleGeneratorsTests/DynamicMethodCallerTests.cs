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
    public class DynamicMethodCallerTests
    {
        [TestMethod()]
        public void CallPropertyGetterTest()
        {
            // arrange
            TestHelper testHelper = new TestHelper("prop1", "prop2");
            DynamicPropertyHandle<TestHelper> ph = PropertyCache<TestHelper>.GetCachedPropertyByCacheKey("Prop1");
            // act
            string result = (string)ph.DynamicPropertyGet(testHelper);
            // assert
            Assert.AreEqual<string>("prop1", result);
        }

        [TestMethod()]
        public void CallPropertySetterTest()
        {
            // arrange
            TestHelper testHelper = new TestHelper("prop1", "prop2");
            DynamicPropertyHandle<TestHelper> ph1 = PropertyCache<TestHelper>.GetCachedPropertyByCacheKey("Prop1");
            DynamicPropertyHandle<TestHelper> ph2 = PropertyCache<TestHelper>.GetCachedPropertyByCacheKey("Prop2");
            // act
            ph1.DynamicPropertySet(testHelper, "newprop1");
            ph2.DynamicPropertySet(testHelper, "newprop2");
            // assert
            Assert.AreEqual("newprop1", testHelper.Prop1);
            Assert.IsNull(ph2.DynamicPropertySet);
            Assert.AreNotEqual("newprop2", testHelper.Prop2);
        }
    }
}