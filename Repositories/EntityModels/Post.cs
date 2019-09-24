﻿using System;
using System.Collections.Generic;

namespace EntityModels
{
    public class Post
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public List<Comment> Comments { get; set; }
        public User Author { get; set; }
    }
}
