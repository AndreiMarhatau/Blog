using System;

namespace EntityModels
{
    public class Token
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string StrToken { get; set; }

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
            return StrToken.GetHashCode();
        }
    }
}
