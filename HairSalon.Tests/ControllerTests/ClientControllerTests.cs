using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests.ControllerTests
{
    [TestClass]
    public class ClientControllerTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }

        [TestMethod]
        public void All_ReturnsCorrectView_True()
        {
            ClientController controller = new ClientController();
            ActionResult allView = controller.All();
            Assert.IsInstanceOfType(allView, typeof(ViewResult));
        }

        [TestMethod]
        public void AddForm_ReturnsCorrectView_True()
        {
            ClientController controller = new ClientController();
            ActionResult addFormView = controller.AddForm();
            Assert.IsInstanceOfType(addFormView, typeof(ViewResult));
        }

        [TestMethod]
        public void Add_ReturnsCorrectView_True()
        {
            ClientController controller = new ClientController();
            ActionResult addView = controller.Add("Jidenna", 1);
            Assert.IsInstanceOfType(addView, typeof(RedirectToActionResult));
        }



    }
}