using BL;
using Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Blog.Services.Tests
{
    public class CommentsAndPostsServiceTests
    {
        [Fact]
        public async void GetCommentsAndPostsByUserId_Add2Posts4CommentsAnd2UsersAndCheckCountAndOrder_Returns2PostsOrderByDescIdAnd4CommentsOrderById()
        {
            //Arrange
            var postsRepo = new Mock<IPostsRepository>();
            postsRepo.Setup(a => a.GetPostsByUserId(1)).ReturnsAsync(GetPostsByUserId(1));

            CommentsAndPostsService commentsAndPostsService = new CommentsAndPostsService(
                postsRepo.Object);
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

        private List<DomainModels.Post> GetPostsByUserId(int id)
        {
            List<DomainModels.Post> posts = new List<DomainModels.Post>();
            posts.AddRange(new List<DomainModels.Post>()
            {
                new DomainModels.Post()
                {
                    Id = 1,
                    Author = GetUserById(1),
                    Date = DateTime.Now,
                    Text = "Пост 1",
                    Comments = new List<DomainModels.Comment>()
                    {

                    },
                    Likes = new List<DomainModels.Like>(){}
                },
                new DomainModels.Post()
                {
                    Id = 2,
                    Author = GetUserById(1),
                    Date = DateTime.Now,
                    Text = "Пост 2",
                    Comments = new List<DomainModels.Comment>()
                    {
                        new DomainModels.Comment()
                        {
                            Id = 2,
                            Author = GetUserById(2),
                            CommentId = 1,
                            PostId = 2,
                            Date = DateTime.Now,
                            Text = "Комментарий 2",
                            Likes = new List<DomainModels.Like>(){}
                        },
                        new DomainModels.Comment()
                        {
                            Id = 3,
                            Author = GetUserById(1),
                            CommentId = 2,
                            PostId = 2,
                            Date = DateTime.Now,
                            Text = "Комментарий 3",
                            Likes = new List<DomainModels.Like>(){}
                        },
                        new DomainModels.Comment()
                        {
                            Id = 1,
                            Author = GetUserById(1),
                            CommentId = -1,
                            PostId = 2,
                            Date = DateTime.Now,
                            Text = "Комментарий 1",
                            Likes = new List<DomainModels.Like>(){}
                        },
                        new DomainModels.Comment()
                        {
                            Id = 4,
                            Author = GetUserById(2),
                            CommentId = 1,
                            PostId = 2,
                            Date = DateTime.Now,
                            Text = "Комментарий 4",
                            Likes = new List<DomainModels.Like>(){}
                        }
                    },
                    Likes = new List<DomainModels.Like>(){}
                }
            });

            return posts;
        }
        private DomainModels.User GetUserById(int id)
        {
            if (id == 1)
            {
                return new DomainModels.User()
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
                return new DomainModels.User()
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
    }
}
