using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests.ControllerTests
{
    [TestClass]
    public class HomeControllerTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }

        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            HomeController controller = new HomeController();
            ActionResult formView = controller.Index();
            Assert.IsInstanceOfType(formView, typeof(ViewResult));
        }
    }
}
