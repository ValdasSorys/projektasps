using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class NewsPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public int WrittenBy { get; set; }

        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;

        public static List<NewsPost> select()
        {
            var NewsPosts = new List<NewsPost>();

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT * FROM news_posts";
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    NewsPosts.Add(new NewsPost() { Id = Reader.GetInt32(0),
                                                   Title = Reader.GetString(1),
                                                   Text = Reader.GetString(2),
                                                   CreateDate = Reader.GetDateTime(3),
                                                   WrittenBy = Reader.GetInt32(4) 
                    });
                }
            }

            return NewsPosts;
        }
    }
}