using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylists
    {
        private int _id;
        private string _name;
        private string _description;

        public Stylists(int id, string name, string description)
        {
            _id = id;
            _name = name;
            _description = description;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetDescription()
        {
            return _description;
        }
    }
}
