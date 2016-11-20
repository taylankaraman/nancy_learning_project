﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nancy_learning_project
{
    public class Post
    {
        public Post()
        {
            Id = 0;
            Name = "";
            Author = "";
        }

        public Post(int id, string name, string author)
        {
            Id = id;
            Name = name;
            Author = author;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }

    public class PostEditModel
    {
        public PostEditModel(string name, string author)
        {
            Name = name;
            Author = author;
        }

        public string Name { get; set; }
        public string Author { get; set; }
    }
}

