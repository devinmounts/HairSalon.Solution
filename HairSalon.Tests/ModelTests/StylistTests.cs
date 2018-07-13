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
    }
}
