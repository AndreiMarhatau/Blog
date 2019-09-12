using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserViewModel(int Id, string Login, string Name, string Surname, DateTime BornDate, DateTime RegisterDate, string Email, string Password)
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
    }
}
