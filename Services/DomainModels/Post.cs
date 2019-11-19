using System;
using System.Collections.Generic;

namespace DomainModels
{
    public class Post
    {
        public Guid Id { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;


        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Likes { get; set; }

        public bool IsValidData()
        {
            if (Text == null ||
                Text.Replace(" ", "").Equals(""))
            {
                return false;
            }
            return true;
        }
    }
}
