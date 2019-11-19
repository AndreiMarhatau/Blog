using System;
using System.Collections.Generic;

namespace BLModels
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public UserInfo Author { get; set; }
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Like> Likes { get; set; }
    }
}
