namespace nancy_learning_project
{
    public class User
    { 
        public string Username { get; private set; }

        public User(string username)
        {
            Username = username;
        }
    }
}