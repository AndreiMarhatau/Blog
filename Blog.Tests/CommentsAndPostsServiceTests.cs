using BL;
using Domain.Core;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Services.Tests
{
    public class CommentsAndPostsServiceTests
    {
        [Fact]
        public async void GetCommentsAndPostsByUserIdTest()
        {
            //Arrange
            RepositoriesForTests repo = new RepositoriesForTests();
            CommentsAndPostsService commentsAndPostsService = new CommentsAndPostsService(
                repo, repo, repo);
            //Act
            var result = commentsAndPostsService.GetCommentsAndPostsByUserId(1);
            //Assert
            Assert.True(
                (await result).Count == 2 &&
                (await result)[0].Id == 2 &&
                (await result)[0].Comments.Count == 4 &&
                (await result)[0].Comments[1].Id == 2
                );
        }

        private class RepositoriesForTests : IUserRepository, ICommentsRepository, IPostsRepository
        {
            public async Task AddComment(Comment post)
            {
                throw new NotImplementedException();
            }

            public async Task AddPost(Post post)
            {
                throw new NotImplementedException();
            }

            public Task AddUser(User user)
            {
                throw new NotImplementedException();
            }

            public async Task<bool> CheckExistsOfUser(string login, string email)
            {
                throw new NotImplementedException();
            }

            public async Task<List<Post>> GetPostsByUserId(int id)
            {
                List<Post> posts = new List<Post>();
                posts.AddRange(new List<Post>()
            {
                new Post()
                {
                    Id = 2,
                    UserId = 1,
                    Date = DateTime.Now,
                    Text = "Пост 2",
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            AuthorId = 1,
                            CommentId = -1,
                            PostId = 2,
                            UserId = 1,
                            Date = DateTime.Now,
                            Text = "Комментарий 1"
                        },
                        new Comment()
                        {
                            Id = 2,
                            AuthorId = 2,
                            CommentId = 1,
                            PostId = 2,
                            UserId = 1,
                            Date = DateTime.Now,
                            Text = "Комментарий 2"
                        },
                        new Comment()
                        {
                            Id = 3,
                            AuthorId = 1,
                            CommentId = 2,
                            PostId = 2,
                            UserId = 1,
                            Date = DateTime.Now,
                            Text = "Комментарий 3"
                        },
                        new Comment()
                        {
                            Id = 4,
                            AuthorId = 2,
                            CommentId = 1,
                            PostId = 2,
                            UserId = 1,
                            Date = DateTime.Now,
                            Text = "Комментарий 4"
                        }
                    }
                },
                new Post()
                {
                    Id = 1,
                    UserId = 1,
                    Date = DateTime.Now,
                    Text = "Пост 1",
                    Comments = new List<Comment>()
                    {

                    }
                }
            });

                return posts;
            }

            public async Task<User> GetUserById(int id)
            {
                if (id == 1)
                {
                    return new User()
                    {
                        Id = 1,
                        BornDate = DateTime.Now,
                        Email = "fsdf@mail.ru",
                        Login = "Andr1o",
                        Name = "Andrey",
                        Surname = "Margatov",
                        RegisterDate = DateTime.Now,
                        Password = "fdsafuhdanfkjwehy fqukywegfh kjbwfwgeqfckhbj"
                    };
                }
                else if (id == 2)
                {
                    return new User()
                    {
                        Id = 2,
                        BornDate = DateTime.Now,
                        Email = "fs1fdsf@mail.ru",
                        Login = "Andr",
                        Name = "Andrey2",
                        Surname = "Margatov2",
                        RegisterDate = DateTime.Now,
                        Password = "fdsafuhdanfkjwehy fqukywegfh kjbwfwgeqfckhbj"
                    };
                }
                else
                    throw new NotImplementedException();
            }

            public async Task<User> GetUserByLogin(string Login)
            {
                throw new NotImplementedException();
            }

            public async Task<List<User>> GetUserListByLoginNameSurname(string Login, string Name, string Surname)
            {
                throw new NotImplementedException();
            }
        }
    }
}
