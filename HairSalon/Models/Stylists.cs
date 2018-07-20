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

        public void DeleteStylist()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

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

        public void Update(string newName, string newDetails)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @newName, details = @newDetails WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = _id;
            cmd.Parameters.Add(thisId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            cmd.Parameters.AddWithValue("@newDetails", newDetails);


            cmd.ExecuteNonQuery();
            _name = newName;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

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

        public void AddBook(Book newBook)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO catalog (book_id, author_id) VALUES (@BookId, @AuthorId);";

            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@BookId";
            book_id.Value = newBook.Id;
            cmd.Parameters.Add(book_id);

            MySqlParameter author_id = new MySqlParameter();
            author_id.ParameterName = "@AuthorId";
            author_id.Value = Id;
            cmd.Parameters.Add(author_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Specialty> GetSpecialties()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists
                                JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
                                JOIN specialties ON (stylists_specialties.specialty_id = specialties.id)
                                WHERE stylists.id = @stylistId;";

            MySqlParameter stylistIdParameter = new MySqlParameter();
            stylistIdParameter.ParameterName = "@stylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add(stylistIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Specialty> specialties = new List<Specialty> { };

            while (rdr.Read())
            {
                int specialtyId = rdr.GetInt32(0);
                string specialtyDescription = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(specialtyDescription, specialtyId);
                specialties.Add(newSpecialty);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return specialties;
        }

        public List<Specialty> GetAllSpecialties()
        {
            return Specialty.GetAll();
        }

    }
}
