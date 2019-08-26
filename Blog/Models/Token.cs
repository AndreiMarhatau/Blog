using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StrToken { get; set; }
    }
}
