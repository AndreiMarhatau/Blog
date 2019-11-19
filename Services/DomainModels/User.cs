using System;

namespace DomainModels
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

        public User() { }

        public User(Guid Id, string Login, string Name, string Surname, DateTime BornDate, DateTime RegisterDate, string Email, string Password)
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
                Login.Replace(" ", "").Equals("") ||
                Name == null ||
                Name.Replace(" ", "").Equals("") ||
                Surname == null ||
                Surname.Replace(" ", "").Equals("") ||
                BornDate == null ||
                Email == null ||
                !Email.Replace(" ", "").Contains("@") ||
                Password == null ||
                Password.Replace(" ", "").Equals(""))
            {
                return false;
            }
            return true;
        }
    }
}
