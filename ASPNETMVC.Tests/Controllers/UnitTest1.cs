using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASPNETMVC;
using ASPNETMVC.Controllers;
using System.Web.Mvc;

namespace ASPNETMVC.Tests.Controllers
{
    [TestClass]
    public class MyFirstMVCControllerTest
    {
        [TestMethod]
        public void SayHello()
        {
            // Arrange
            MyFirstMVCController controller = new MyFirstMVCController();
            // Act
            ActionResult view = controller.SayHello();
            // Assert
            Assert.IsNotNull(view);
        }
    }
}
