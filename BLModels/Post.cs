using System;
using System.Collections.Generic;

namespace BLModels
{
    public class Post
    {
        public Guid Id { get; set; }
        public UserInfo Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }
    }
}
