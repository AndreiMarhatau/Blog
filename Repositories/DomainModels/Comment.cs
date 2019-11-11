using System;
using System.Collections.Generic;

namespace DomainModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public User Author { get; set; }
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Like> Likes { get; set; }

        public bool IsValidData()
        {
            if (Text == null ||
                Text.Replace(" ", "").Equals("") ||
                Author.IsValidData())
            {
                return false;
            }
            return true;
        }
    }
}
