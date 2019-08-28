using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StrToken { get; set; }

        public bool IsValidData()
        {
            if (StrToken == null)
            {
                return false;
            }
            return true;
        }
    }
}
