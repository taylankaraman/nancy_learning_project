using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using Nancy;
using Nancy.Responses;
using System.Web.Script.Serialization;

namespace nancy_learning_project
{
    public class HomeModule : NancyModule
    {
        InDbPostRepository posts = new InDbPostRepository();

        public HomeModule()
        {
            Get["/"] = parameters => View["index.html"];             
            Get["/posts"] = parameters => Response.AsJson(posts.GetAll());
            Get["/posts/{id}"] = parameters => Response.AsJson(posts.GetById((int)parameters.id));
            Post["/newpostdb/{name}/{author}"] = CreatePost;
        }

        private dynamic CreatePost(dynamic parameters)
        {            
            String name = parameters.name;
            String author = parameters.author;
            var newPost = new PostEditModel(name, author);            

            return "New post created in DB: \n" + new JavaScriptSerializer().Serialize(posts.Create(newPost)); ;
        }
    }
}

