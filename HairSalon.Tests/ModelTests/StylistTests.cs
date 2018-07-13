using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistsTests
    {
        public void FoodTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=devin_mounts_test;";
        }

        [TestMethod]
        public void GetSetId_SetsGetsId_Int()
        {
            Stylist newStylist = new Stylist(1, "Brandy", "Works Monday");

            Assert.AreEqual(1, newStylist.GetId());
        }

        [TestMethod]
        public void GetSetName_SetsGetsName_String()
        {
            Stylist newStylist = new Stylist(1, "Brandy", "Works Monday");

            Assert.AreEqual("Brandy", newStylist.GetName());
        }

        [TestMethod]
        public void GetSetDescription_SetsGetsDescription_String()
        {
            Stylist newStylist = new Stylist(1, "Brandy", "Works Monday");

            Assert.AreEqual("Works Monday", newStylist.GetDescription());
        }

    }
}
