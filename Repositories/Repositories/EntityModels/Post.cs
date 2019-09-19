using System;
using System.Collections.Generic;

namespace Repositories
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public List<Comment> Comments { get; set; }
        public User Author { get; set; }
    }
}
