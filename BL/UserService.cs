using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Core;
using Interfaces;
using IServices;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Dictionary<string, string>>> SearchUsers(string Login, string Name, string Surname)
        {
            //Check params
            if (Name == null) Name = "";
            if (Surname == null) Surname = "";
            if (Login == null) Login = "";

            //Create result list of search
            var resultList = (await _userRepository.GetUserList())
                .Where(
                i => i.Name.ToLower().Contains(Name.ToLower()) &&
                i.Surname.ToLower().Contains(Surname.ToLower()) &&
                i.Login.ToLower().Contains(Login.ToLower())).ToList();

            //Create string result from result list
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            foreach (var i in resultList)
            {
                result.Add(
                    new Dictionary<string, string>()
                    {
                        { "Id", i.Id.ToString() },
                        { "Login", i.Login },
                        { "Name", i.Name },
                        { "Surname", i.Surname },
                        { "BornDate", i.BornDate.ToShortDateString() },
                        { "RegisterDate", i.RegisterDate.ToLongDateString() },
                        { "Email", i.Email },
                        { "Password", i.Password },
                    }
                    );
            }
            return result;
        }

        public async Task<int> AddUser(string Login, string Name, string Surname, DateTime BornDate, string Email, string Password)
        {
            User user = new User()
            {
                Login = Login,
                Name = Name,
                Surname = Surname,
                BornDate = BornDate,
                Email = Email,
                Password = Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)))
            };
            var isA = user.IsValidData();
            var isB = (await _userRepository.GetUserList()).Where(i => i.Login == Login || i.Email == Email).Count() == 0;
            if (isA && isB)
            {
                await _userRepository.AddUser(user);
                return (await _userRepository.GetUserList()).Single(i => i.Login == Login).Id;
            }
            else
                throw new ArgumentException("Invalid arguments");
        }

        public async Task<int> CheckUser(string Login, string Password)
        {
            var user = (await _userRepository.GetUserList())
                .Single(i => i.Login == Login && i.Password ==
                Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password))));
            return user.Id;
        }

        public async Task<Dictionary<string, string>> GetUserById(int id)
        {
            User user = (await _userRepository.GetUserList()).Single(i => i.Id == id);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Id", user.Id.ToString());
            dict.Add("Login", user.Login);
            dict.Add("Name", user.Name);
            dict.Add("Surname", user.Surname);
            dict.Add("BornDate", user.BornDate.ToShortDateString());
            dict.Add("RegisterDate", user.RegisterDate.ToLongDateString());
            dict.Add("Email", user.Email);
            dict.Add("Password", user.Password);

            return dict;
        }

        public async Task<Dictionary<string, string>> GetUserByLogin(string Login)
        {
            User user = (await _userRepository.GetUserList()).Single(i => i.Login == Login);

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Id", user.Id.ToString());
            dict.Add("Login", user.Login);
            dict.Add("Name", user.Name);
            dict.Add("Surname", user.Surname);
            dict.Add("BornDate", user.BornDate.ToShortDateString());
            dict.Add("RegisterDate", user.RegisterDate.ToLongDateString());
            dict.Add("Email", user.Email);
            dict.Add("Password", user.Password);

            return dict;
        }
    }
}
