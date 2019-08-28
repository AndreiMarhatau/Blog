using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsValidData()
        {
            if (Login == null ||
                Name == null ||
                Surname == null ||
                BornDate == null ||
                Email == null ||
                !Email.Replace(" ", "").Contains("@") ||
                Password == null)
            {
                return false;
            }
            return true;
        }
    }
}
