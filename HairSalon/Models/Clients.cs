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

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> { };

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                int StylistId = rdr.GetInt32(2);
                string Name = rdr.GetString(1);
                Client newClient = new Client(Id, StylistId, Name);
                allClients.Add(newClient);
            }
            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            return allClients;
        }
    }
}
