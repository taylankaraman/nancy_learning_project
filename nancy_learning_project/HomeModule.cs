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

            Get["/"] = p => "Welcome to Nancy Blog";            

            Get["/posts"] = p => Response.AsJson(posts.All());

            Get["/posts/{id}"] = p => Response.AsJson(posts.GetById((int)p.id));

        }
    }
}