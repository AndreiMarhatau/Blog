using System;
using System.Collections.Generic;
using System.Text;

namespace BLModels
{
    public class Like
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int UserId { get; set; }
    }
}
