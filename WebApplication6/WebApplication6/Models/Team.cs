using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class Team
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;
        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchCount { get; set; }
        public int WinCount { get; set; }
        public int IsRemoved { get; set; }

        public static List<Team> select()
        {
            var Team = new List<Team>();

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT * FROM team";
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    Team.Add(new Team()
                    {
                        Id = Reader.GetInt32(0),
                        Name = Reader.GetString(1),
                        MatchCount = Reader.GetInt32(2),
                        WinCount = Reader.GetInt32(3),
                        IsRemoved = Reader.GetInt32(4)
                    });
                }
            }

            Reader.Close();
            Connection.Close();

            return Team;
        }
        public static Team select(int id)
        {

            var team = new Team();

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT * FROM team where id = " + id;
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    team.Id = Reader.GetInt32(0);
                    team.Name = Reader.GetString(1);
                    team.MatchCount = Reader.GetInt32(2);
                    team.WinCount = Reader.GetInt32(3);
                    team.IsRemoved = Reader.GetInt32(4);
                }
            }

            Reader.Close();
            Connection.Close();

            return team;
        }
    }

}