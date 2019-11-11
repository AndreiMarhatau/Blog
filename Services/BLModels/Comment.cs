using System;
using System.Collections.Generic;

namespace BLModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public UserInfo Author { get; set; }
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Like> Likes { get; set; }
    }
}
