using System;
using System.Collections.Generic;

namespace EntityModels
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public List<Comment> Comments { get; set; }
        public User Author { get; set; }
        public List<Like> Likes { get; set; }
    }
}
