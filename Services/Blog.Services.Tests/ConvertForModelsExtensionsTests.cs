using Domain.Core;
using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Services.Tests
{
    public class ConvertForModelsExtensionsTests
    {
        [Fact]
        public void ToUserViewModel_ConvertUserToUserViewModel()
        {
            //Arrange
            User user = new User()
            {
                Id=1,
                BornDate=DateTime.MinValue,
                Email="test@mail.ru",
                Login="login",
                Name="name",
                Surname="surname",
                Password="password",
                RegisterDate=DateTime.MinValue
            };
            //Act
            var userViewModel = user.ToUserViewModel();
            //Assert
            Assert.True(
                userViewModel.Id == user.Id &&
                userViewModel.BornDate == user.BornDate &&
                userViewModel.Email == user.Email &&
                userViewModel.Login == user.Login &&
                userViewModel.Name == user.Name &&
                userViewModel.Surname == user.Surname &&
                userViewModel.Password == user.Password &&
                userViewModel.RegisterDate == user.RegisterDate
                );
        }

        [Fact]
        public void ToPostViewModel_ConvertPostToPostViewModel()
        {
            //Arrange
            Post post = new Post()
            {
                Id = 1,
                Comments = null,
                Date = DateTime.Now,
                Text = "",
                UserId = 2
            };
            //Act
            var postViewModel = post.ToPostViewModel(
                new List<CommentInPost>(),
                new UserInfo()
                {
                    Name ="name",
                    Surname ="surname"
                });
            //Assert
            Assert.True(
                postViewModel.Id == post.Id &&
                postViewModel.Comments.Count == 0 &&
                postViewModel.Date == post.Date &&
                postViewModel.Text == post.Text &&
                postViewModel.UserId == post.UserId &&
                postViewModel.UserName == "name" &&
                postViewModel.UserSurname == "surname"
                );
        }

        [Fact]
        public void ToCommentInPost_ConvertCommentToCommentInPost()
        {
            //Arrange
            Comment comment = new Comment()
            {
                Id = 1,
                Date = DateTime.Now,
                Text = "",
                UserId = 2,
                AuthorId=1,
                CommentId=-1,
                PostId=1
            };
            //Act
            var commentViewModel = comment.ToCommentInPost(
                new UserInfo()
                {
                    Name = "name",
                    Surname = "surname"
                });
            //Assert
            Assert.True(
                commentViewModel.Id == comment.Id &&
                commentViewModel.Date == comment.Date &&
                commentViewModel.Text == comment.Text &&
                commentViewModel.UserId == comment.UserId &&
                commentViewModel.CommentId == comment.CommentId &&
                commentViewModel.PostId == comment.PostId &&
                commentViewModel.AuthorId == comment.AuthorId &&
                commentViewModel.AuthorName == "name" &&
                commentViewModel.AuthorSurname == "surname"
                );
        }

        [Fact]
        public void ToUserInfo_ConvertUserToUserInfo()
        {
            //Arrange
            User user = new User()
            {
                Id = 1,
                BornDate = DateTime.MinValue,
                Email = "test@mail.ru",
                Login = "login",
                Name = "name",
                Surname = "surname",
                Password = "password",
                RegisterDate = DateTime.MinValue
            };
            //Act
            var userInfo = user.ToUserInfo();
            //Assert
            Assert.True(
                userInfo.Id == user.Id &&
                userInfo.Name == user.Name &&
                userInfo.Surname == user.Surname
                );
        }
    }
}
