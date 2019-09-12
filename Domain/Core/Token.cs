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

        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (this == obj)
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            if (this.UserId == ((Token)obj).UserId &&
               this.StrToken == ((Token)obj).StrToken)
                return true;
            return ((Object)this).Equals(obj);
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
