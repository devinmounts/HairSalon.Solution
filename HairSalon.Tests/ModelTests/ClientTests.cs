using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests
    {
        public void FoodTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=devin_mounts_test;";
        }

        [TestMethod]
        public void GetSetId_SetsGetsId_Int()
        {
            Client newClient = new Client(1, 1, "Jidenna");

            Assert.AreEqual(1, newClient.GetId());
        }

        [TestMethod]
        public void GetSetDescription_SetsGetsDescription_String()
        {
            Client newClient = new Client(1, 1, "Jidenna");

            Assert.AreEqual(1, newClient.GetStylistId());
        }

        [TestMethod]
        public void GetSetName_SetsGetsName_String()
        {
            Client newClient = new Client(1, 1, "Jidenna");

            Assert.AreEqual("Jidenna", newClient.GetName());
        }

        [TestMethod]
        public void GetAll_DBStartsEmpty_0()
        {
            int result = Client.GetAll().Count;
            Assert.AreEqual(0, result);
        }
 

    }
}

