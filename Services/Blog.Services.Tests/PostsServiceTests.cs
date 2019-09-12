﻿using BL;
using Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Services.Tests
{
    public class PostsServiceTests
    {
        [Fact]
        public async void AddPost_AddInvalidPost()
        {
            //Arrange
            var mockRepository = new Mock<IPostsRepository>();
            var postsService = new PostsService(mockRepository.Object);
            //Act
            var result = postsService.AddPost(0, "");
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
    }
}
