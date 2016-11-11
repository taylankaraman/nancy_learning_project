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

        static List<Post> List = new List<Post>(new[]
        {
            new Post(0, "Getting Started with Nancy", "Taylan Karaman"),
            new Post(1, "HTTP Fundamentals", "Khan Thompson"),
        });
    }

    public class InDbPostRepository : PostRepository
    {
        int post_seq = 4;

        static List<Post> List = new List<Post>(new[]
{
            new Post(0, "Getting Started with Nancy", "Taylan Karaman"),
            new Post(1, "HTTP Fundamentals", "Khan Thompson"),
        });


        string strConnection = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString.ToString();

        public void insert(PostEditModel post)
        {
            using (SqlConnection con = new SqlConnection(strConnection))
            {
                string sql = "insert into Table values('" + post.Name + "','" + post.Author + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();                
            }
        }

        public Post getById(int id)
        {
            String Name = "";
            String Author = "";

            using (SqlConnection con = new SqlConnection(strConnection))
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
            insert(post);

            Post newPost = new Post(post_seq, post.Name, post.Author);
            return newPost;            
        }

        public Post GetById(int id)
        {            
            return getById(id);
        }
    }


}