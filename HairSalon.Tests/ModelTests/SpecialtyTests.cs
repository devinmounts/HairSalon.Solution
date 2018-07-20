using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtyTests : IDisposable
    {
        public void Dispose()
        {
            Specialty.DeleteAll();
        }

        public void SpecialtiesTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=devin_mounts_test;";
        }

        [TestMethod]
        public void GetSetId_SetsGetsId_Int()
        {
            Specialty newSpecialty = new Specialty("braids", 1);

            Assert.AreEqual(1, newSpecialty.Id);
        }

        [TestMethod]
        public void GetSetDescription_SetsGetsDescription_String()
        {
            Specialty newSpecialty = new Specialty("braids", 1);

            Assert.AreEqual("braids", newSpecialty.Description);
        }

        [TestMethod]
        public void GetAll_DBStartsEmpty_0()
        {
            int result = Specialty.GetAll().Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfSameSpecialty_Bool()
        {
            Specialty firstSpecialty = new Specialty("braids", 1);
            Specialty secondSpecialty = new Specialty("braids", 1);

            Assert.AreEqual(firstSpecialty, secondSpecialty);
        }

        [TestMethod]
        public void Save_SaveSpecialtyToDatabase_SpecialtyList()
        {
            Specialty newSpecialty = new Specialty("braids", 1);
            newSpecialty.Save();

            List<Specialty> testList = new List<Specialty> { newSpecialty };
            List<Specialty> result = Specialty.GetAll();

            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_SaveAssignsIdToObject_Id()
        {
            Specialty newSpecialty = new Specialty("braids", 1);
            newSpecialty.Save();

            Specialty savedSpecialty = Specialty.GetAll()[0];
            int result = savedSpecialty.Id;
            int testId = newSpecialty.Id;

            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsIdOfObject_Id()
        {
            Specialty savedSpecialty = new Specialty("braids", 1);
            savedSpecialty.Save();

            Specialty testSpecialty = Specialty.Find(1);

            int result = savedSpecialty.Id;
            int testId = testSpecialty.Id;

            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void DeleteAll_DeletesAllSpecialtysInDatabase_SpecialtyList()
        {
            
            Specialty testSpecialty01 = new Specialty("braids", 1);
            testSpecialty01.Save();
            Specialty testSpecialty02 = new Specialty("locs", 2);
            testSpecialty02.Save();

            Specialty.DeleteAll();
            List<Specialty> expectedList = new List<Specialty> { };
            List<Specialty> resultList = Specialty.GetAll();



            CollectionAssert.AreEqual(expectedList, resultList);
        }

        [TestMethod]
        public void Delete_DeletesSpecialtyInDatabase_Specialty()
        {

            Specialty testSpecialty01 = new Specialty("braids", 1);
            testSpecialty01.Save();
            Specialty testSpecialty02 = new Specialty("braids", 2);
            testSpecialty02.Save();

            testSpecialty01.DeleteSpecialty();
            List<Specialty> expectedList = new List<Specialty> { testSpecialty02 };
            List<Specialty> resultList = Specialty.GetAll();



            CollectionAssert.AreEqual(expectedList, resultList);
        }

        [TestMethod]
        public void Update_UpdatesSpecialtyInDatabase_Specialty()
        {
            Specialty testSpecialty01 = new Specialty("braids", 1);
            testSpecialty01.Save();

            testSpecialty01.Update("tight braids");

            Assert.AreEqual("tight braids", testSpecialty01.Description);
        }

        [TestMethod]
        public void AddGetStylist_AddsGetsStylistOfSpecialtyInDatabase_StylistList()
        {
            Stylist testStylist02 = new Stylist(2, "Jidenna", "Works Monday");
            testStylist02.Save();
            Specialty newSpecialty = new Specialty("locs", 2);
            newSpecialty.Save();

            newSpecialty.AddStylist(testStylist02);
            List<Stylist> specialtiesStylists = newSpecialty.GetStylists();
            List<Stylist> testList = new List<Stylist> { testStylist02 };
            Console.WriteLine(testList.Count);
            Console.WriteLine(specialtiesStylists.Count);

            CollectionAssert.AreEqual(testList, specialtiesStylists);
        }
    }
}

