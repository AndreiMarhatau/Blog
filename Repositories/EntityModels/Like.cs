﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid? PostId { get; set; }
        public Guid? CommentId { get; set; }
        public Guid UserId { get; set; }

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
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
