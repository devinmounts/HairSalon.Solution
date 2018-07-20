using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistsTests : IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
        }
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

            Assert.AreEqual("Works Monday", newStylist.GetDetails());
        }

        [TestMethod]
        public void GetAll_DBStartsEmpty_0()
        {
            int result = Stylist.GetAll().Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfSameStylist_Bool()
        {
            Stylist firstStylist = new Stylist(1, "Jidenna", "Works Monday");
            Stylist secondStylist = new Stylist(1, "Jidenna", "Works Monday");

            Assert.AreEqual(firstStylist, secondStylist);
        }

        [TestMethod]
        public void Save_SaveStylistToDatabase_StylistList()
        {
            Stylist newStylist = new Stylist(1, "Jidenna", "Works Monday");
            newStylist.Save();

            List<Stylist> testList = new List<Stylist> { newStylist };
            List<Stylist> result = Stylist.GetAll();

            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_SaveAssignsIdToObject_Id()
        {
            Stylist newStylist = new Stylist(1, "Jidenna", "Works Monday");
            newStylist.Save();

            Stylist savedStylist = Stylist.GetAll()[0];
            int result = savedStylist.GetId();
            int testId = newStylist.GetId();

            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsStylistInDatabase_Stylist()
        {
            
            Stylist testStylist = new Stylist(1, "Jidenna", "Works Monday");
            testStylist.Save();


            Stylist foundStylist = Stylist.Find(testStylist.GetId());

           
            Assert.AreEqual(testStylist, foundStylist);
        }

        [TestMethod]
        public void FindStylistClients_FindsStylistClientsInDatabase_List()
        {

            Stylist testStylist = new Stylist(1, "Jidenna", "Works Monday");
            testStylist.Save();
            Client testClient = new Client(1, "Donna", 1);
            testClient.Save();
            List<Client> testList = new List<Client> { testClient };


            List<Client> stylistClients = testStylist.FindClients();


            CollectionAssert.AreEqual(testList, stylistClients);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllStylistsInDatabase_StylistList()
        {

            Stylist testStylist01 = new Stylist(1, "Jidenna", "Works Monday");
            testStylist01.Save();
            Stylist testStylist02 = new Stylist(2, "Salmon", "Works on the River");
            testStylist02.Save();

            Stylist.DeleteAll();
            List<Stylist> expectedList = new List<Stylist> { };
            List<Stylist> resultList = Stylist.GetAll();



            CollectionAssert.AreEqual( expectedList, resultList );
        }

        [TestMethod]
        public void Delete_DeletesStylistInDatabase_Stylist()
        {

            Stylist testStylist01 = new Stylist(1, "Jidenna", "Works Monday");
            testStylist01.Save();
            Stylist testStylist02 = new Stylist(2, "Salmon", "Works on the River");
            testStylist02.Save();

            testStylist01.DeleteStylist();
            List<Stylist> expectedList = new List<Stylist> { testStylist02};
            List<Stylist> resultList = Stylist.GetAll();



            CollectionAssert.AreEqual(expectedList, resultList);
        }

        [TestMethod]
        public void Update_UpdatesStylistInDatabase_Stylist()
        {
            Stylist testStylist01 = new Stylist(1, "Jidenna", "Works Monday");
            testStylist01.Save();

            testStylist01.Update("Jedenna", "Beautiful Hair");

            Assert.AreEqual("Jedenna", testStylist01.GetName());
        }
    }
}

