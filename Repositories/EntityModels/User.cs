using System;

namespace EntityModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
