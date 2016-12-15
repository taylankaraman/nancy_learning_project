namespace nancy_learning_project
{
    using Nancy;
    using Nancy.Security;    

    public class SecureModule : NancyModule
    {
        public SecureModule() : base("/secure")
        {
            this.RequiresAuthentication();

            Get["/"] = parameters =>
            {
                var model = new User(this.Context.CurrentUser.UserName);
                return View["secure.cshtml", model];
            };
        }
    }
}