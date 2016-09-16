using System;
using System.Collections.Generic;
using System.Linq;

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
}