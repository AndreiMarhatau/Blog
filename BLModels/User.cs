using System;

namespace BLModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Email { get; set; }

        public User() { }

        public User(Guid Id, string Login, string Name, string Surname, DateTime BornDate, DateTime RegisterDate, string Email)
        {
            this.Id = Id;
            this.Login = Login;
            this.Name = Name;
            this.Surname = Surname;
            this.BornDate = BornDate;
            this.RegisterDate = RegisterDate;
            this.Email = Email;
        }
    }
}
