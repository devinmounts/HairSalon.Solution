using System;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public static class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}
