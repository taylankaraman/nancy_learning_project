using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Nancy;
using Nancy.Responses;

namespace nancy_learning_project
{
    public class HomeModule : NancyModule
    {

        public HomeModule()
        {
            var posts = new InMemoryPostRepository();

            Get["/"] = parameters => "Welcome to Nancy Blog";            

            Get["/posts"] = parameters => Response.AsJson(posts.All());

            Get["/posts/{id}"] = parameters => Response.AsJson(posts.GetById((int)parameters.id));

            Post["/newpost/{name}/{author}"] = CreatePost;

        }

        private dynamic CreatePost(dynamic parameters)
        {
            var posts = new InMemoryPostRepository();

            String name = parameters.name;
            String author = parameters.author;
            var newPost = new PostEditModel(name, author);

            posts.Create(newPost);

            return "New post created";

        }

    }
}

// 1. Modify Web.config to integrate with a DB
// 2. Implement a POST method that passes a new post data to the app.