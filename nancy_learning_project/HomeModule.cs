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
        InDbPostRepository posts = new InDbPostRepository();

        public HomeModule()
        {
            // var posts = new InMemoryPostRepository();

            Get["/"] = parameters => View["index.html"];            

            // Get["/posts"] = parameters => Response.AsJson(posts.GetAll());

            Get["/posts/{id}"] = parameters => Response.AsJson(posts.GetById((int)parameters.id));

            Post["/newpost/{name}/{author}"] = CreatePost; //Postman

            // DB requests
            Post["/newpostdb/{name}/{author}"] = CreatePostDB; //Postman
            Get["/posts"] = parameters => Response.AsJson(posts.GetAll()); //Postman

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

        private dynamic CreatePostDB(dynamic parameters)
        {            
            String name = parameters.name;
            String author = parameters.author;
            var newPost = new PostEditModel(name, author);

            posts.Create(newPost);

            return "New post created in DB";
        }
    }
}

