using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class Request
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;
        public DateTime CreationDate { get; set; }
        public int Team_id { get; set; }
        public int Player_id { get; set; }

        public static Request select(int id)
        {

            var request = new Request();

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT * FROM request where player_id = " + id;
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    request.CreationDate = Reader.GetDateTime(0);
                    request.Player_id = Reader.GetInt32(1);
                    request.Team_id = Reader.GetInt32(2);
                }
            }

            Reader.Close();
            Connection.Close();

            return request;
        }


        public static void createRequest(Team team)
        {
            string userID = HttpContext.Current.Session["id"].ToString();
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            DateTime rightNow = DateTime.Now;
            string nowString = rightNow.Year + "-" + rightNow.Month + "-" + rightNow.Day;
            string SQLStatement = string.Format("INSERT INTO request ( creationDate, team_id, player_id) VALUES ('{0}', '{1}', '{2}');", nowString ,team.Id, userID);
            var Command = new MySqlCommand(SQLStatement, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }


    }
}