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
        public void ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=devin_mounts_test;";
        }

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

        [TestMethod]
        public void Details_ReturnsCorrectView_True()
        {
            ClientController controller = new ClientController();
            ActionResult detailsView = controller.Details(1);
            Assert.IsInstanceOfType(detailsView, typeof(ViewResult));
        }

        [TestMethod]
        public void DeleteAll_ReturnsCorrectView_True()
        {
            StylistController controller = new StylistController();
            ActionResult deleteAllView = controller.DeleteAll();
            Assert.IsInstanceOfType(deleteAllView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Delete_ReturnsCorrectView_True()
        {
            StylistController controller = new StylistController();
            ActionResult deleteView = controller.DeleteAll();
            Assert.IsInstanceOfType(deleteView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void UpdateForm_ReturnsCorrectView_True()
        {
            StylistController controller = new StylistController();
            ActionResult updateView = controller.UpdateForm(1);
            Assert.IsInstanceOfType(updateView, typeof(ViewResult));
        }

        [TestMethod]
        public void Update_ReturnsCorrectView_True()
        {
            StylistController controller = new StylistController();
            ActionResult updateView = controller.Update(1, "name", "details");
            Assert.IsInstanceOfType(updateView, typeof(RedirectToActionResult));
        }
      


    }
}