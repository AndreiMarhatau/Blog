using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int AuthorId { get; set; }
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
