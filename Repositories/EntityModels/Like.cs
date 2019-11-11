using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels
{
    public class Like
    {
        public int Id { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int UserId { get; set; }

        public override bool Equals(object obj)
        {
            var tmp = (Like)obj;
            if (Id == tmp.Id) return true;
            if (PostId == tmp.PostId &&
                UserId == tmp.UserId) return true;
            if (CommentId == tmp.CommentId &&
                UserId == tmp.UserId) return true;
            return false;
        }
    }
}
