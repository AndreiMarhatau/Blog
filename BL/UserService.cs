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

        public async Task<List<UserViewModel>> SearchUsers(string Login, string Name, string Surname)
        {
            //Check params
            if (Name == null) Name = "";
            if (Surname == null) Surname = "";
            if (Login == null) Login = "";

            //Create result list of search
            var resultList = await _userRepository.GetUserListByLoginNameSurname(Login, Name, Surname);

            //Create dictionary from result list
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var i in resultList)
            {
                result.Add(
                    new UserViewModel
                    (
                        i.Id,
                        i.Login,
                        i.Name,
                        i.Surname,
                        i.BornDate,
                        i.RegisterDate,
                        i.Email,
                        i.Password
                    ));
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

            if (!User.Password.Equals(Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)))))
                throw new InvalidOperationException("Incorrect password");

            return User.Id;
        }

        public async Task<UserViewModel> GetUserById(int id)
        {
            User i = await _userRepository.GetUserById(id);

            UserViewModel user = new UserViewModel
                    (
                        i.Id,
                        i.Login,
                        i.Name,
                        i.Surname,
                        i.BornDate,
                        i.RegisterDate,
                        i.Email,
                        i.Password
                        );

            return user;
        }

        public async Task<UserViewModel> GetUserByLogin(string Login)
        {
            User i = await _userRepository.GetUserByLogin(Login);

            UserViewModel user = new UserViewModel
                    (
                        i.Id,
                        i.Login,
                        i.Name,
                        i.Surname,
                        i.BornDate,
                        i.RegisterDate,
                        i.Email,
                        i.Password
                        );

            return user;
        }
    }
}
