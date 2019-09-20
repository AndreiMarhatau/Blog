using System;

namespace DomainModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public User Author { get; set; }
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        

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
