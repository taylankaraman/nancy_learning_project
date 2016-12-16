
namespace nancy_learning_project
{
    public class Post
    {
        public Post()
        {
            Id = 0;
            Name = "";
            Author = "";
            Content = "";
        }

        public Post(int id, string name, string author, string content)
        {
            Id = id;
            Name = name;
            Author = author;
            Content = content;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }

    public class PostEditModel
    {
        public PostEditModel(string name, string author, string content)
        {
            Name = name;
            Author = author;
            Content = content;
        }

        public string Name { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}

