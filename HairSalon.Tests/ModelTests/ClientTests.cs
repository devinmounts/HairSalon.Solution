﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }
        public void ClientsTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=devin_mounts_test;";
        }

        [TestMethod]
        public void GetSetId_SetsGetsId_Int()
        {
            Client newClient = new Client(1, "Jidenna", 1);

            Assert.AreEqual(1, newClient.GetId());
        }

        [TestMethod]
        public void GetSetDescription_SetsGetsDescription_String()
        {
            Client newClient = new Client(1, "Jidenna", 1);

            Assert.AreEqual(1, newClient.GetStylistId());
        }

        [TestMethod]
        public void GetSetName_SetsGetsName_String()
        {
            Client newClient = new Client(1, "Jidenna", 1);

            Assert.AreEqual("Jidenna", newClient.GetName());
        }

        [TestMethod]
        public void GetAll_DBStartsEmpty_0()
        {
            int result = Client.GetAll().Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfSameClient_Bool()
        {
            Client firstClient = new Client(1, "Jidenna", 1);
            Client secondClient = new Client(1, "Jidenna", 1);

            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void Save_SaveClientToDatabase_ClientList()
        {
            Client newClient = new Client(1, "Jidenna", 1);
            newClient.Save();

            List<Client> testList = new List<Client> { newClient };
            List<Client> result = Client.GetAll();

            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_SaveAssignsIdToObject_Id()
        {
            Client newClient = new Client(1, "Jidenna", 1);
            newClient.Save();

            Client savedClient = Client.GetAll()[0];
            int result = savedClient.GetId();
            int testId = newClient.GetId();

            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsIdOfObject_Id()
        {
            Client newClient = new Client(1, "Jidenna", 1);
            newClient.Save();

            Client savedClient = Client.Find(1);

            int result = savedClient.GetId();
            int testId = newClient.GetId();

            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void FindClientStylist_FindsClientStylistInDatabase_Stylist()
        {

            Stylist testStylist = new Stylist(1, "Jidenna", "Works Monday");
            testStylist.Save();
            Client testClient = new Client(1, "Donna", 1);
            testClient.Save();


            Stylist clientStylist = testClient.FindStylist();


            Assert.AreEqual(testStylist, clientStylist);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllClientsInDatabase_ClientList()
        {

            Client testClient01 = new Client(1, "Jidenna", 2);
            testClient01.Save();
            Client testClient02 = new Client(2, "Salmon", 2);
            testClient02.Save();

            Client.DeleteAll();
            List<Client> expectedList = new List<Client> { };
            List<Client> resultList = Client.GetAll();



            CollectionAssert.AreEqual(expectedList, resultList);
        }

        [TestMethod]
        public void Delete_DeletesClientInDatabase_Client()
        {

            Client testClient01 = new Client(1, "Jidenna", 2);
            testClient01.Save();
            Client testClient02 = new Client(2, "Salmon", 4);
            testClient02.Save();

            testClient01.DeleteClient();
            List<Client> expectedList = new List<Client> { testClient02 };
            List<Client> resultList = Client.GetAll();



            CollectionAssert.AreEqual(expectedList, resultList);
        }

        [TestMethod]
        public void Update_UpdatesClientInDatabase_Client()
        {
            Client testClient01 = new Client(1, "Jidenna", 3);
            testClient01.Save();

            testClient01.Update("Jedenna", 3);

            Assert.AreEqual("Jedenna", testClient01.GetName());
        }
    }
}

