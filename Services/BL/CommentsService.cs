﻿using Domain.Core;
using Interfaces;
using IServices;
using System;
using System.Threading.Tasks;

namespace BL
{
    public class CommentsService:ICommentsService
    {
        private ICommentsRepository _commentsRepository;

        public CommentsService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task AddComment(int PostId, int UserId, int AuthorId, int CommentId, string Text)
        {
            Comment comment = new Comment()
            {
                PostId = PostId,
                UserId = UserId,
                Author = new User() { Id = AuthorId },
                CommentId = CommentId,
                Text = Text
            };

            if (comment.IsValidData())
                await _commentsRepository.AddComment(comment);
            else
                throw new ArgumentException("Invalid arguments");
        }
    }
}
