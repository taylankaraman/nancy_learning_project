namespace nancy_learning_project
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Linq;
    using Nancy;
    using Nancy.Responses;
    using System.Web.Script.Serialization;
    using Nancy.Authentication.Forms;
    using Nancy.Extensions;
    using System.Dynamic;

    public class HomeModule : NancyModule
    {
        InDbPostRepository posts = new InDbPostRepository();

        public HomeModule()
        {
            Get["/"] = parameters => View["index.html"];             
            Get["/posts"] = parameters => Response.AsJson(posts.GetAll());
            Get["/posts/{id}"] = parameters => Response.AsJson(posts.GetById((int)parameters.id));
            Post["/createpost/{name}/{author}/{content}"] = CreatePostDB;
            Get["/editpost"] = EditPost;
            Post["/editpost"] = CreatePost;

            Get["/login"] = parameters =>
            {               
                return View["login"];
            };
            


            Post["/login"] = parameters => {
                var userGuid = UserDatabase.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return this.Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                }

                return View["edit_post.html"];
                // return this.LoginAndRedirect(userGuid.Value);
            };

            Get["/logout"] = parameters => {
                return this.LogoutAndRedirect("~/");
            };
        }

        private dynamic CreatePostDB(dynamic parameters)
        {            
            String name = parameters.name;
            String author = parameters.author;
            String content = parameters.content;
            var newPost = new PostEditModel(name, author, content);            

            return "New post created via API: \n" + new JavaScriptSerializer().Serialize(posts.Create(newPost)); ;
        }

        private dynamic CreatePost(dynamic parameters)
        {
            

            String postName = (string)Request.Form.Name;
            String Author = (string)Request.Form.Author;
            String Content = (string)Request.Form.Content;
            var newPost = new PostEditModel(postName, Author, Content);

            return "New post created from GUI: \n" + new JavaScriptSerializer().Serialize(posts.Create(newPost)); ;
        }

        private dynamic EditPost(dynamic parameters)
        {
            return View["edit_post.html"];            
        }
    }
}

