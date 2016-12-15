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
            Post["/newpostdb/{name}/{author}"] = CreatePost;


            Get["/login"] = parameters =>
            {
                dynamic model = new ExpandoObject();
                // model.Errored = this.Request.Query.error.HasValue;

                return View["login", model];
            };
            


            Post["/login"] = parameters => {
                var userGuid = UserDatabase.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return this.Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/logout"] = parameters => {
                return this.LogoutAndRedirect("~/");
            };
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

