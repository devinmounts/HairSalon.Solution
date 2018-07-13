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
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

        public override bool Equals(System.Object otherClient)
        {

            if (!(otherClient is Client))
            {
                return false;
            }
            else
            {
                Client newClient = (Client)otherClient;
                bool idEquality = (this.GetId() == newClient.GetId());
                bool nameEquality = (this.GetName() == newClient.GetName());
                bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
                return (idEquality && nameEquality && stylistIdEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (id, name, stylist_id) VALUES (@thisId, @thisName, @thisStylist_Id;";

            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@thisId";
            id.Value = this._id;
            cmd.Parameters.Add(id);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@thisName";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@thisStylist_Id";
            stylist_id.Value = this._stylistId;
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
