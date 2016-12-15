using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nancy_learning_project.Models
{
    public class Class1 : IUserIdentity
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }

}