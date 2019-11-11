using System;
using System.Collections.Generic;

namespace EntityModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? AuthorId { get; set; }
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public User Author { get; set; }
        public List<Like> Likes { get; set; }
    }
}
