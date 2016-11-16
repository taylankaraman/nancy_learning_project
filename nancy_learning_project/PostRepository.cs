using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

namespace nancy_learning_project
{
    public interface PostRepository
    {
        IEnumerable<Post> All();

        Post GetById(int id);

        Post Create(PostEditModel post);
    }

    public class InMemoryPostRepository : PostRepository
    {
        static List<Post> List = new List<Post>(new[]
        {
            new Post(0, "Getting Started with Nancy", "Taylan Karaman"),
            new Post(1, "HTTP Fundamentals", "Khan Thompson"),
        });

        public IEnumerable<Post> All()
        {
            return List;
        }

        public Post Create(PostEditModel post)
        {
            long maxId = List.LongCount();
            Post newPost = new Post((int)maxId++, post.Name, post.Author);
            List.Add(newPost);

            return newPost;
        }

        public Post GetById(int id)
        {
            return List.FirstOrDefault(Post => Post.Id == id);
        }
    }

    public class InDbPostRepository : PostRepository
    {
        // initialized
        int post_seq = 4;
        string connectionString = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString.ToString();
        //----


        public void Insert(PostEditModel post)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO [Table] values('" + post.Name + "','" + post.Author + "')";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();              
            }
        }

        public Post GetById(int id)
        {
            String Name = "";
            String Author = "";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "select Name, Author from Table where Id=" + id + ";";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    Name = reader[0].ToString();
                    Author = reader[1].ToString();
                }
                finally
                {                    
                    reader.Close();
                }
                con.Close();

                return new Post(id, Name, Author);
            }
        }


        public IEnumerable<Post> All()
        {
            List<Post> List = new List<Post>();

            //        string querystring =
            //"select orderid, customerid from dbo.orders;";
            //        using (sqlconnection connection = new sqlconnection(
            //                   connectionstring))
            //        {
            //            sqlcommand command = new sqlcommand(
            //                querystring, connection);
            //            connection.open();
            //            sqldatareader reader = command.executereader();
            //            try
            //            {
            //                while (reader.read())
            //                {
            //                    console.writeline(string.format("{0}, {1}",
            //                        reader[0], reader[1]));
            //                }
            //            }
            //            finally
            //            {
            //                reader.close();
            //            }

            //            connection.close();
            //        }

            return List;
        }

        public Post Create(PostEditModel post)
        {
            Insert(post);

            Post newPost = new Post(post_seq, post.Name, post.Author);
            return newPost;            
        }

    }


}