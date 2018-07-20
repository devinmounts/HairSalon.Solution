using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Description { get; set; }



        public Specialty(string description, int id)
        {
            Id = id;
            Description = description;
        }

        public override int GetHashCode()
        {
            return this.Description.GetHashCode();
        }

        public override bool Equals(System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            }
            else
            {
                Specialty newSpecialty = (Specialty)otherSpecialty;
                bool idEquality = (this.Id == newSpecialty.Id);
                bool descriptionEquality = (this.Description == newSpecialty.Description);

                return (idEquality && descriptionEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties (id, description) VALUES (@ThisId, @Description);";

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@Description";
            description.Value = Description;
            cmd.Parameters.Add(description);

            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@ThisId";
            id.Value = Id;
            cmd.Parameters.Add(id);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialties = new List<Specialty> { };

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Description = rdr.GetString(1);
                Specialty newSpecialty = new Specialty(Description, Id);
                allSpecialties.Add(newSpecialty);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allSpecialties;
        }

        public static Specialty Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `specialties` WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int specialtyId = 0;
            string description = string.Empty;

            while (rdr.Read())
            {
                specialtyId = rdr.GetInt32(0);
                description = rdr.GetString(1);
            }

            Specialty foundSpecialty = new Specialty(description, specialtyId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundSpecialty;
        }

        public void Update(string newDescription)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE specialties SET description = @newDescription WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@newDescription";
            description.Value = newDescription;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();

            Description = newDescription;


            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void DeleteSpecialty()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties WHERE id = @SpecialtyId; DELETE FROM stylists_specialties WHERE specialty_id = @SpecialtyId;";

            MySqlParameter specialtyIdParameter = new MySqlParameter();
            specialtyIdParameter.ParameterName = "@SpecialtyId";
            specialtyIdParameter.Value = Id;
            cmd.Parameters.Add(specialtyIdParameter);

            cmd.ExecuteNonQuery();
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM specialties;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = newStylist.GetId();
            cmd.Parameters.Add(stylist_id);

            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@SpecialtyId";
            specialty_id.Value = Id;
            cmd.Parameters.Add(specialty_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Stylist> GetStylists()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT stylists.* FROM specialties
                                JOIN stylists_specialties ON (specialties.id = stylists_specialties.specialty_id)
                                JOIN stylists ON (stylists_specialties.stylist_id = stylists.id)
                                WHERE specialties.id = @specialtyId;";

            MySqlParameter specialtyIdParameter = new MySqlParameter();
            specialtyIdParameter.ParameterName = "@specialtyId";
            specialtyIdParameter.Value = Id;
            cmd.Parameters.Add(specialtyIdParameter);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Stylist> stylists = new List<Stylist> { };

            while (rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string stylistDetails = rdr.GetString(2);
                Stylist newStylist = new Stylist(stylistId, stylistName, stylistDetails);
                stylists.Add(newStylist);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return stylists;
        }

        public List<Stylist> GetAllSpecialties()
        {
            return Stylist.GetAll();
        }
    }
}
