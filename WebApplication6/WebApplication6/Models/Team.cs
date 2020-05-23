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

        public static List<Team> getTeams()
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

        public static void create(Team team)
        {
            string userID = HttpContext.Current.Session["id"].ToString();
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = string.Format("INSERT INTO team ( name, matchCount, winCount, isRemoved) VALUES ('{0}', '{1}', '{2}', '0');", team.Name, 0, 0);
            var Command = new MySqlCommand(SQLStatement, Connection);
            Command.ExecuteNonQuery();
            SQLStatement = string.Format("INSERT INTO `team_member` (`isCaptain`, `player_id`, `team_id`) VALUES( '1', '{0}', (SELECT MAX(id) FROM team ))", userID);
            Command = new MySqlCommand(SQLStatement, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        public static List<String> getTeamInfo(int id)
        {
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = string.Format("SELECT isuser.nickname, team_member.isCaptain FROM team_member LEFT JOIN isuser ON team_member.player_id = isuser.id WHERE team_member.team_id = {0}", id);
            var command = new MySqlCommand(SQLStatement, Connection);
            command.ExecuteNonQuery();
            MySqlDataReader Reader = command.ExecuteReader();
            var teamMembers = new List<String>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    int teamRole = Reader.GetInt32(1);
                    if (teamRole == 1)
                        teamMembers.Add(Reader.GetString(0) + " - Captain");
                    else
                        teamMembers.Add(Reader.GetString(0));
                }
            }

            Reader.Close();
            Connection.Close();

            return teamMembers;
        }

        public static bool canPlayerCreate()
        {
            string userID = HttpContext.Current.Session["id"].ToString();
            bool result = true;
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = string.Format("SELECT * FROM team_member WHERE team_member.player_id = {0}", userID);
            var command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = command.ExecuteReader();
            if (Reader.HasRows)
            {
                result = false;
            }
            Reader.Close();

            SQLStatement = string.Format("SELECT * FROM request WHERE request.player_id = {0}", userID);
            command = new MySqlCommand(SQLStatement, Connection);
            Reader = command.ExecuteReader();
            if (Reader.HasRows)
            {
                result = false;
            }


            Reader.Close();
            Connection.Close();
            return result;
        }

        public static int getRole(Team team)
        {
            string userID = HttpContext.Current.Session["id"].ToString();
            int result = 0;
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = string.Format("SELECT * FROM team_member WHERE team_member.player_id = {0} AND team_member.team_id = {1}", userID, team.Id);
            var command = new MySqlCommand(SQLStatement, Connection);
            command.ExecuteNonQuery();
            MySqlDataReader Reader = command.ExecuteReader();
            var teamMembers = new List<String>();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    int teamRole = Reader.GetInt32(0);
                    if (teamRole == 1)
                        result = 2;
                    else if (teamRole == 0)
                        result = 1;
                }
            }

            Reader.Close();
            Connection.Close();

            return result;
        }

        public static void addTeamMember(Request request)
        {
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = string.Format("INSERT INTO team_member (isCaptain, player_id, team_id) VALUES (0,{0},{1})", request.Player_id, request.Team_id);
            var command = new MySqlCommand(SQLStatement, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }


        public int getTeamMemberCount(Team team)
        {
            int result = 0;
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = string.Format("SELECT * FROM team_member WHERE team_member.team_id = {0}",team.Id);
            var command = new MySqlCommand(SQLStatement, Connection);
            command.ExecuteNonQuery();
            MySqlDataReader Reader = command.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    result++;
                }
            }

            Reader.Close();
            Connection.Close();

            return result;
        }


    }

}