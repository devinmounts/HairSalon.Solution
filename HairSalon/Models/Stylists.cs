using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _description;

        public Stylist(int id, string name, string description)
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
                string Description = rdr.GetString(2);
                Stylist newStylist = new Stylist(Id, Name, Description);
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
                bool descriptionEquality= (this.GetDescription() == newStylist.GetDescription());
                return (idEquality && nameEquality && descriptionEquality);
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
            cmd.CommandText = @"INSERT INTO stylist (id, name, description) VALUES (@thisId, @thisName, @thisDescription);";

            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@thisId";
            id.Value = this._id;
            cmd.Parameters.Add(id);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@thisName";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@thisStylist_Id";
            description.Value = this._description;
            cmd.Parameters.Add(description);

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
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE searchId = @thisId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@thisId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int Id = 0;
            string Name = String.Empty;
            string Description = String.Empty;
            while (rdr.Read())
            {
                Id = rdr.GetInt32(0);
                Name = rdr.GetString(1);
                Description = rdr.GetString(2);
            }

            Stylist foundStylist = new Stylist(Id, Name, Description);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundStylist;
        }

    }
}
