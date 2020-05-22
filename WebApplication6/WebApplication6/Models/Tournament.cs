using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class Tournament
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;
        public int Id { get; set; }
        public int TournamentCreator { get; set; }
        public int PlayerCount { get; set; }
        public DateTime StartDate { get; set; }
        public int MaxPlayerCount { get; set; }
        public string Name { get; set; }
        public bool isPlaying { get; set; }
        public bool isCaptain { get; set; }
        public int teamID { get; set; }
        public static void update(int id, string user)
        {
            var Tournament = new Tournament();
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT team_id FROM team_member where player_id = " + HttpContext.Current.Session["id"].ToString();

            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                Tournament.Id = Reader.GetInt32(0);
            }
            Reader.Close();
            Connection.Close();
            Connection.Open();


            SQLStatement = "INSERT INTO team_tournament_participation (tournament_id, team_id) VALUES ("+id+" , "+Tournament.Id+" )";
            Command = new MySqlCommand(SQLStatement, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }
        public static List<Tournament> select()
        {
            var Tournament = new List<Tournament>();

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT * FROM tournament"; 
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();
        
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    Tournament.Add(new Tournament()
                    {
                        Id = Reader.GetInt32(0),
                        TournamentCreator = Reader.GetInt32(1),
                        PlayerCount = Reader.GetInt32(2),
                        StartDate = Reader.GetDateTime(3),
                        MaxPlayerCount = Reader.GetInt32(4),
                        Name = Reader.GetString(5)
                    });
                }
            }

            Reader.Close();
            Connection.Close();

            return Tournament;
        }
        public static Tournament select(int id )
        {
            var Tournament = new Tournament();

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT * FROM tournament where id = "+id;
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {

                Reader.Read();
                Tournament.Id = Reader.GetInt32(0);
                Tournament.TournamentCreator = Reader.GetInt32(1);
                Tournament.PlayerCount = Reader.GetInt32(2);
                Tournament.StartDate = Reader.GetDateTime(3);
                Tournament.MaxPlayerCount = Reader.GetInt32(4);
                Tournament.Name = Reader.GetString(5);
            }
            Reader.Close();
            
            SQLStatement = "SELECT team_id, isCaptain FROM team_member where player_id = " + HttpContext.Current.Session["id"].ToString();
            Command = new MySqlCommand(SQLStatement, Connection);
            Reader = Command.ExecuteReader();


            if (Reader.HasRows)
            {
                Reader.Read();
                Tournament.teamID = Reader.GetInt32(0);
                int isCaptain = Reader.GetInt32(1);
                if (isCaptain == 1)
                {
                    Tournament.isCaptain = true;
                }
                else Tournament.isCaptain = false;
                Reader.Close();
                if (Tournament.isCaptain)
                {
                    SQLStatement = "SELECT * FROM team_tournament_participation where team_id = " + Tournament.teamID +" AND tournament_id = "+id;
                    Command = new MySqlCommand(SQLStatement, Connection);
                    Reader = Command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        Reader.Read();
                        Tournament.isPlaying = true;
                        Reader.Close();
                    }
                    else Tournament.isPlaying = false;
                    Command = new MySqlCommand(SQLStatement, Connection);

                }
            }
            Connection.Close();
            return Tournament;
        }
    }

}