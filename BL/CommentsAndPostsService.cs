﻿using Domain.Core;
using Interfaces;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CommentsAndPostsService : ICommentsAndPostsService
    {
        private ICommentsRepository _commentsRepository;
        private IPostsRepository _postsRepository;
        private IUserRepository _userRepository;

        public CommentsAndPostsService(ICommentsRepository commentsRepository, IPostsRepository postsRepository, IUserRepository userRepository)
        {
            _commentsRepository = commentsRepository;
            _postsRepository = postsRepository;
            _userRepository = userRepository;
        }

        public async Task<List<PostWithComments>>
            GetCommentsAndPostsByUserId(int id)
        {
            List<Post> posts = await _postsRepository.GetPostsByUserId(id);

            List<PostWithComments> postsWithComments = new List<PostWithComments>();
            User user = await _userRepository.GetUserById(id);

            foreach (var post in posts)
            {
                //Preparation comments
                List<CommentInPost> commentsInPost = new List<CommentInPost>();
                foreach (var comment in post.Comments)
                {
                    var author = await _userRepository.GetUserById(comment.AuthorId);
                    commentsInPost.Add(new CommentInPost(
                        comment.Id,
                        author.Name,
                        author.Surname,
                        comment.PostId,
                        comment.UserId,
                        comment.AuthorId,
                        comment.Text,
                        comment.Date,
                        comment.CommentId
                        //Add other information if needed
                    ));
                }

                //Preparation post and add to result list
                postsWithComments.Add(new PostWithComments(
                    post.Id,
                    user.Name,
                    user.Surname,
                    post.UserId,
                    post.Text,
                    post.Date
                    //Add other information if needed
                , commentsInPost));
            }

            return postsWithComments;
        }
    }
}
