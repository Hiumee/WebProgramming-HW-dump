using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AspCoreMVCEF.Models;
using MySql.Data.MySqlClient;

namespace AspCoreMVCEF.DataAbstractionLayer
{
    public class DAL
    {
        public List<Article> GetArticlesFromCategory(string category)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            List<Article> slist = new List<Article>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM news WHERE Category = '" + category + "'";
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Article article = new Article();
                    article.Id = myreader.GetInt32("Id");
                    article.Title = myreader.GetString("Title");
                    article.Content = myreader.GetString("Content");
                    article.Author = myreader.GetInt32("Author");
                    article.Category = myreader.GetString("Category");
                    article.Date = myreader.GetDateTime("Date");
                    slist.Add(article);
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return slist;

        }

        public Article GetArticleById(int id, int author_id)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            Article rarticle = null;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM news WHERE Id = " + id + " AND Author = '" + author_id + "'";
                MySqlDataReader myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    Article article = new Article();
                    article.Id = myreader.GetInt32("Id");
                    article.Title = myreader.GetString("Title");
                    article.Content = myreader.GetString("Content");
                    article.Author = myreader.GetInt32("Author");
                    article.Category = myreader.GetString("Category");
                    article.Date = myreader.GetDateTime("Date");
                    rarticle = article;
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return rarticle;
        }

        public List<Article> GetArticlesByDate(DateTime date1, DateTime date2)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            List<Article> slist = new List<Article>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM news INNER JOIN users ON users.ID = news.Author WHERE Date BETWEEN '" + date1.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND '" + date2.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Article article = new Article();
                    article.Id = myreader.GetInt32("Id");
                    article.Title = myreader.GetString("Title");
                    article.Content = myreader.GetString("Content");
                    article.Author = myreader.GetInt32("Author");
                    article.Category = myreader.GetString("Category");
                    article.Date = myreader.GetDateTime("Date");
                    slist.Add(article);
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return slist;
        }

        internal void UpdateArticle(int id, string title, string category, string content)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            List<string> slist = new List<string>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE news SET Title = '" + title + "', Content = '" + content + "', Category = '" + category + "' WHERE ID = " + id;
                cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
        }

        internal int GetAuthorByName(string username)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            int uname = 0;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Id FROM users WHERE Username = '" + username + "'";
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    uname = myreader.GetInt32("Id");
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return uname;
        }

        internal string CheckCredentials(string username, string password)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            string uname = "";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Username FROM users WHERE Username = '" + username + "' AND Password = '" + password + "'";
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    uname = myreader.GetString("Username");
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return uname;
        }

        public List<string> GetCategories()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            List<string> slist = new List<string>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT DISTINCT Category FROM news";
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    string category = myreader.GetString("Category");
                    slist.Add(category);
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return slist;
        }

        public void AddArticle(string title, string category, int author, string content, DateTime date)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            List<string> slist = new List<string>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO news (Title, Content, Author, Category, Date) VALUES ('"+title+"','"+content+"',"+author+",'"+category+"','"+date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";
                cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
        }

        public int UserId(string username)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=lab7;";
            int id = -1;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Id FROM users WHERE Username = '" + username + "'";
                MySqlDataReader myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    id = myreader.GetInt32("Id");
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return id;
        }
    }
}
