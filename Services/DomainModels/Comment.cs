using System;
using System.Collections.Generic;

namespace DomainModels
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public User Author { get; set; }
        public Guid CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

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
