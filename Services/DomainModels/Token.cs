﻿using System;

namespace DomainModels
{
    public class Token
    {
        public Guid UserId { get; set; }
        public string StrToken { get; set; }

        public bool IsValidData()
        {
            if (StrToken == null || UserId == Guid.Empty)
            {
                return false;
            }
            return true;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            if (obj.GetType() != typeof(Token)) return false;
            var Obj = (Token)obj;
            if (Obj.StrToken == this.StrToken && Obj.UserId == this.UserId) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return StrToken.GetHashCode() ^ UserId.GetHashCode();
        }
    }
}
