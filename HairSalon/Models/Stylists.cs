using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _details;

        public Stylist(int id, string name, string details)
        {
            _id = id;
            _name = name;
            _details = details;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetDetails()
        {
            return _details;
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist> { };

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                string Details = rdr.GetString(2);
                Stylist newStylist = new Stylist(Id, Name, Details);
                allStylists.Add(newStylist);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStylists;
        }

        public override bool Equals(System.Object otherStylist)
        {

            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist)otherStylist;
                bool idEquality = (this.GetId() == newStylist.GetId());
                bool nameEquality = (this.GetName() == newStylist.GetName());
                bool detailsEquality= (this.GetDetails() == newStylist.GetDetails());
                return (idEquality && nameEquality && detailsEquality);
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
            cmd.CommandText = @"INSERT INTO stylists (id, name, details) VALUES (@thisId, @thisName, @thisdetails);";

            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@thisId";
            id.Value = this._id;
            cmd.Parameters.Add(id);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@thisName";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter details = new MySqlParameter();
            details.ParameterName = "@thisdetails";
            details.Value = this._details;
            cmd.Parameters.Add(details);

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
            cmd.CommandText = @"DELETE FROM stylists;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteStylist(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE * FROM stylists WHERE id = @thisId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@thisId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int Id = 0;
            string Name = String.Empty;
            string Details = String.Empty;

            while (rdr.Read())
            {
                Id = rdr.GetInt32(0);
                Name = rdr.GetString(1);
                Details = rdr.GetString(2);
            }
            Stylist newStylist = new Stylist(Id, Name, Details);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newStylist;
        }

        public List<Client> FindClients()
        {
            List<Client> stylistClients  = new List<Client> { };

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients where stylist_id = " + this._id + " ;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            //MySqlParameter searchId = new MySqlParameter();
            //searchId.ParameterName = "@thisId";
            //searchId.Value = this._id;
            //cmd.Parameters.Add(searchId);

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);
                int StylistId = rdr.GetInt32(2);
                Client foundClient = new Client(Id, Name, StylistId);
                stylistClients.Add(foundClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return stylistClients;
        }

    }
}
