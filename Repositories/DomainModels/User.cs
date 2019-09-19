using System;

namespace DomainModels
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

        public User() { }

        public User(int Id, string Login, string Name, string Surname, DateTime BornDate, DateTime RegisterDate, string Email, string Password)
        {
            this.Id = Id;
            this.Login = Login;
            this.Name = Name;
            this.Surname = Surname;
            this.BornDate = BornDate;
            this.RegisterDate = RegisterDate;
            this.Email = Email;
            this.Password = Password;
        }

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
