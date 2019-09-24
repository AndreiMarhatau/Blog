using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityModels
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? AuthorId { get; set; }
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        
        public User Author { get; set; }
        public Post Post { get; set; }
    }
}
