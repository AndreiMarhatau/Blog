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
        private IUserRepository _userRepository;

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
            var resultList = await _userRepository.GetUserListByLoginNameSurname(Login, Name, Surname);

            //Create dictionary from result list
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

            if (user.IsValidData() &&
                !(await _userRepository.CheckExistsOfUser(Login, Email)))
            {
                await _userRepository.AddUser(user);
                return (await _userRepository.GetUserByLogin(Login)).Id;
            }
            else
                throw new ArgumentException("Invalid arguments");
        }

        public async Task<int> CheckUser(string Login, string Password)
        {
            var User = await _userRepository.GetUserByLogin(Login);

            if (User.Password != Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password))))
                throw new InvalidOperationException("Incorrect password");

            return User.Id;
        }

        public async Task<Dictionary<string, string>> GetUserById(int id)
        {
            User user = await _userRepository.GetUserById(id);

            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                { "Id", user.Id.ToString() },
                { "Login", user.Login },
                { "Name", user.Name },
                { "Surname", user.Surname },
                { "BornDate", user.BornDate.ToShortDateString() },
                { "RegisterDate", user.RegisterDate.ToLongDateString() },
                { "Email", user.Email },
                { "Password", user.Password }
            };

            return dict;
        }

        public async Task<Dictionary<string, string>> GetUserByLogin(string Login)
        {
            User user = await _userRepository.GetUserByLogin(Login);

            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                { "Id", user.Id.ToString() },
                { "Login", user.Login },
                { "Name", user.Name },
                { "Surname", user.Surname },
                { "BornDate", user.BornDate.ToShortDateString() },
                { "RegisterDate", user.RegisterDate.ToLongDateString() },
                { "Email", user.Email },
                { "Password", user.Password }
            };

            return dict;
        }
    }
}
