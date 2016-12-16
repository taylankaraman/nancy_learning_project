using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace nancy_learning_project
{
    public interface PostRepository
    {
        IEnumerable<Post> GetAll();

        Post GetById(int id);

        Post Create(PostEditModel post);
    }

    public class InDbPostRepository : PostRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["BlogDB"].ConnectionString.ToString();

        public Post GetById(int id)
        {            
            Post postFound = new Post();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = String.Format("SELECT Id, Name, Author from [Table] where Id='{0}';",id);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        postFound.Id = (int)reader[0];
                        postFound.Name = reader[1].ToString();
                        postFound.Author = reader[2].ToString();
                    }                    
                }
                finally
                {                    
                    reader.Close();
                }
                connection.Close();

                return postFound;
            }
        }


        public IEnumerable<Post> GetAll()
        {
            List<Post> List = new List<Post>();
            return GetAllPosts(List);
        }

        public Post Create(PostEditModel post)
        {
            int insertedId = InsertPost(post);            
            return new Post(insertedId, post.Name, post.Author, post.Content);
        }
                        
        public int InsertPost(PostEditModel post)
        {
            int lastId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertCmd = String.Format("INSERT INTO [Table] values('{0}','{1}','{2}');"
                                   + "SELECT SCOPE_IDENTITY();", post.Name, post.Author, post.Content);                                   

                SqlCommand command = new SqlCommand(insertCmd, connection);
                connection.Open();
                lastId = Convert.ToInt32(command.ExecuteScalar().ToString());                
                connection.Close();                
            }
            return lastId;
        }

        public List<Post> GetAllPosts(List<Post> List)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))               
            {
                string sql = "SELECT Id, Name, Author, Content from [Table] order by Id desc;";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        List.Add(new Post((int)reader[0], (String)reader[1], (String)reader[2], (String)reader[3]));                        
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