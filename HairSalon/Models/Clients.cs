using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private int _stylistId;
        private string _name;

        public Client(int id, int stylistId, string name)
        {
            _id = id;
            _stylistId = stylistId;
            _name = name;
        }

        public int GetId()
        {
            return _id;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
