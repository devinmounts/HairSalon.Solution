using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests.ControllerTests
{
    [TestClass]
    public class StylisControllerTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }

        [TestMethod]
        public void All_ReturnsCorrectView_True()
        {
            StylistController controller = new StylistController();
            ActionResult allView = controller.All();
            Assert.IsInstanceOfType(allView, typeof(ViewResult));
        }

        [TestMethod]
        public void AddForm_ReturnsCorrectView_True()
        {
            StylistController controller = new StylistController();
            ActionResult addFormView = controller.AddForm();
            Assert.IsInstanceOfType(addFormView, typeof(ViewResult));
        }

        [TestMethod]
        public void Add_ReturnsCorrectView_True()
        {
            StylistController controller = new StylistController();
            ActionResult addView = controller.Add("Jidenna", "Works Monday");
            Assert.IsInstanceOfType(addView, typeof(RedirectToActionResult));
        }
      
    }
}
