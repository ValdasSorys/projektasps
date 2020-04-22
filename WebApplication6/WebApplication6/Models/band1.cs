using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WebApplication6.Models
{
    public class band1
    {
        public int id { get; set; }
        public string pavadinimas { get; set; }

        string connStr = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        public band1()
        {
            id = 1;
            pavadinimas = connStr;
        }

        public band1(int _id)
        {
            var con = new MySqlConnection(connStr);
            con.Open();
            string sql = "SELECT * FROM band1 WHERE id = " + _id;
            var cmd = new MySqlCommand(sql, con);

            MySqlDataReader rdr = cmd.ExecuteReader();
            cmd = new MySqlCommand(sql, con);
            rdr.Read();
            if (rdr.HasRows)
            {
                id = rdr.GetInt32(0);
                pavadinimas = rdr.GetString(1);
            }
            else
            {
                id = -1;
                pavadinimas = "nerado";
            }
            rdr.Close();
            con.Close();
        }
    }
}