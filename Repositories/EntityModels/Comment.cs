using System;
using System.Collections.Generic;

namespace EntityModels
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public User Author { get; set; }
        public List<Like> Likes { get; set; }
    }
}
