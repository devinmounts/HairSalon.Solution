using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Client
    {
        private int _id;
        private string _name;
        private int _stylistId;

        public Client(int id, string name, int stylistId)
        {
            _id = id;
            _name = name;
            _stylistId = stylistId;
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
                string Name = rdr.GetString(1);
                int StylistId = rdr.GetInt32(2);
                Client newClient = new Client(Id, Name, StylistId);
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

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (id, name, stylist_id) VALUES (@thisId, @thisName, @thisStylist_Id);";

            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@thisId";
            id.Value = _id;
            cmd.Parameters.Add(id);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@thisName";
            name.Value =_name;
            cmd.Parameters.Add(name);

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@thisStylist_Id";
            stylist_id.Value = _stylistId;
            cmd.Parameters.Add(stylist_id);

            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteClient()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@thisId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@thisId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int Id = 0;
            string Name = String.Empty;
            int StylistId = 0;
            while (rdr.Read())
            {
                Id = rdr.GetInt32(0);
                Name = rdr.GetString(1);
                StylistId = rdr.GetInt32(2);
            }

            Client foundClient = new Client(Id, Name, StylistId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundClient;
        }

        public void Update(string newName, int newStylistId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE clients SET name = @newName, stylist_Id = @newStylistId WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = _id;
            cmd.Parameters.Add(thisId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            cmd.Parameters.AddWithValue("newStylistId", newStylistId);

            cmd.ExecuteNonQuery();
            _name = newName;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }

        public Stylist FindStylist()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisStylistId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@thisStylistId";
            searchId.Value = this._stylistId;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int Id = 0;
            string Name = String.Empty;
            string Details = String.Empty;
            while(rdr.Read())
            {
                Id = rdr.GetInt32(0);
                Name = rdr.GetString(1);
                Details = rdr.GetString(2);
            }

            Stylist foundStylist = new Stylist(Id, Name, Details);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundStylist;
        }

    }
}
