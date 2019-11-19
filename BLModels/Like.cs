using System;
using System.Collections.Generic;
using System.Text;

namespace BLModels
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid LikableId { get; set; }
        public Guid UserId { get; set; }
        public LikableType LikableType { get; set; }
    }
}
