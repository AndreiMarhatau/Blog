using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Helpers;
using Interfaces;
using IServices;

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
                result.Add(i.ToUserViewModel());
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

            UserViewModel user = i.ToUserViewModel();

            return user;
        }

        public async Task<UserViewModel> GetUserByLogin(string Login)
        {
            User i = await _userRepository.GetUserByLogin(Login);

            UserViewModel user = i.ToUserViewModel();

            return user;
        }
    }
}
