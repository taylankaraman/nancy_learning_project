using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

namespace nancy_learning_project
{
    public interface PostRepository
    {
        IEnumerable<Post> GetAll();

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

        public IEnumerable<Post> GetAll()
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


        public IEnumerable<Post> GetAll()
        {
            List<Post> List = new List<Post>();
            return GetAllPosts(List);
        }

        public Post Create(PostEditModel post)
        {
            InsertPost(post);

            Post newPost = new Post(post_seq, post.Name, post.Author);
            return newPost;            
        }


        // SQL Operations
        public void InsertPost(PostEditModel post)
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

        public List<Post> GetAllPosts(List<Post> List)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))               
            {
                string sql = "SELECT Id, Name, Author from [Table];";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        List.Add(new Post((int)reader[0], (String)reader[1], (String)reader[2]));                        
                    }
                }
                finally
                {
                    reader.Close();
                }

                connection.Close();
            }

            return List;
        }




    }


}