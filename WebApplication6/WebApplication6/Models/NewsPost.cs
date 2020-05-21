using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace WebApplication6.Models
{
    public class NewsPost
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(4000)]
        public string Text { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public string WrittenBy { get; set; }

        private static string ConnectionString = ConfigurationManager.ConnectionStrings["mySQLConnection"].ConnectionString;

        public static List<NewsPost> select()
        {
            var NewsPosts = new List<NewsPost>();

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT news_posts.id, title, text, news_posts.createDate, ISUser.nickname FROM news_posts " +
                "INNER JOIN administrators ON administrators.id = news_posts.fk_writtenBy INNER JOIN ISUSer ON ISUser.id = administrators.id";
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
                                                   WrittenBy = Reader.GetString(4) 
                    });
                }
            }

            Reader.Close();
            Connection.Close();

            return NewsPosts;
        }

        public static NewsPost select(int id)
        {
            NewsPost NP = null;

            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "SELECT news_posts.id, title, text, news_posts.createDate, ISUser.nickname FROM news_posts " +
                "INNER JOIN administrators ON administrators.id = news_posts.fk_writtenBy INNER JOIN ISUSer ON ISUser.id = administrators.id WHERE news_posts.id=" + id;
            var Command = new MySqlCommand(SQLStatement, Connection);
            MySqlDataReader Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                NP = new NewsPost()
                {
                    Id = Reader.GetInt32(0),
                    Title = Reader.GetString(1),
                    Text = Reader.GetString(2),
                    CreateDate = Reader.GetDateTime(3),
                    WrittenBy = Reader.GetString(4)
                };
            }

            Reader.Close();
            Connection.Close();

            return NP;
        }

        public static void delete(int id)
        {
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = "DELETE FROM news_posts WHERE id=" + id;
            var Command = new MySqlCommand(SQLStatement, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        public static void update(NewsPost NP)
        {
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            string SQLStatement = string.Format("UPDATE news_posts SET title='{0}', text='{1}', createDate='{2}' WHERE id='{3}'", NP.Title, NP.Text, NP.CreateDate, NP.Id);
            var Command = new MySqlCommand(SQLStatement, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        public static void create(NewsPost NP)
        {
            var Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
            //TODO: find the administrators id
            string SQLStatement = string.Format("INSERT INTO news_posts ( title, text, createDate, fk_writtenBy) VALUES ('{0}', '{1}', '{2}', '1');", NP.Title, NP.Text, NP.CreateDate);
            var Command = new MySqlCommand(SQLStatement, Connection);
            Command.ExecuteNonQuery();
            Connection.Close();
        }
    }
}