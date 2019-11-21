using BL;
using DALInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Blog.Services.Tests
{
    public class CommentsAndPostsServiceTests
    {
        Guid guid = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        Guid guid3 = Guid.NewGuid();
        Guid guid4 = Guid.NewGuid();
        [Fact]
        public async void GetCommentsAndPostsByUserId_Add2Posts4CommentsAnd2UsersAndCheckCountAndOrder_Returns2PostsOrderByDescIdAnd4CommentsOrderById()
        {
            var postsRepo = new Mock<IPostsRepository>();
            postsRepo.Setup(a => a.GetPostsByUserId(guid)).ReturnsAsync(GetPostsByUserId(guid));

            CommentsAndPostsService commentsAndPostsService = new CommentsAndPostsService(
                postsRepo.Object);

            var result = commentsAndPostsService.GetCommentsAndPostsByUserId(guid);
            var result1 = await result;

            Assert.True(
                (await result).Count == 2 &&
                (await result)[0].Id == guid2 &&
                (await result)[0].Comments.Count == 4 &&
                (await result)[0].Comments[1].Id == guid3
                );
        }

        private List<DomainModels.Post> GetPostsByUserId(Guid id)
        {
            List<DomainModels.Post> posts = new List<DomainModels.Post>();
            posts.AddRange(new List<DomainModels.Post>()
            {
                new DomainModels.Post()
                {
                    Id = guid,
                    Author = GetUserById(guid),
                    Date = DateTime.Now,
                    Text = "Пост 1",
                    Comments = new List<DomainModels.Comment>()
                    {

                    },
                    Likes = new List<DomainModels.Like>(){}
                },
                new DomainModels.Post()
                {
                    Id = guid2,
                    Author = GetUserById(guid),
                    Date = DateTime.Now,
                    Text = "Пост 2",
                    Comments = new List<DomainModels.Comment>()
                    {
                        new DomainModels.Comment()
                        {
                            Id = guid2,
                            Author = GetUserById(guid2),
                            CommentId = guid,
                            PostId = guid2,
                            Date = DateTime.Now,
                            Text = "Комментарий 2",
                            Likes = new List<DomainModels.Like>(){}
                        },
                        new DomainModels.Comment()
                        {
                            Id = guid3,
                            Author = GetUserById(guid),
                            CommentId = guid2,
                            PostId = guid2,
                            Date = DateTime.Now,
                            Text = "Комментарий 3",
                            Likes = new List<DomainModels.Like>(){}
                        },
                        new DomainModels.Comment()
                        {
                            Id = guid,
                            Author = GetUserById(guid),
                            CommentId = Guid.Empty,
                            PostId = guid2,
                            Date = DateTime.Now,
                            Text = "Комментарий 1",
                            Likes = new List<DomainModels.Like>(){}
                        },
                        new DomainModels.Comment()
                        {
                            Id = guid4,
                            Author = GetUserById(guid2),
                            CommentId = guid,
                            PostId = guid2,
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
        private DomainModels.User GetUserById(Guid id)
        {
            if (id == guid)
            {
                return new DomainModels.User()
                {
                    Id = guid,
                    BornDate = DateTime.Now,
                    Email = "fsdf@mail.ru",
                    Login = "Andr1o",
                    Name = "Andrey",
                    Surname = "Margatov",
                    RegisterDate = DateTime.Now,
                    Password = "fdsafuhdanfkjwehy fqukywegfh kjbwfwgeqfckhbj"
                };
            }
            else if (id == guid2)
            {
                return new DomainModels.User()
                {
                    Id = guid2,
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
