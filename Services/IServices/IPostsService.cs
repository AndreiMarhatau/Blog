﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IPostsService
    {
        Task AddPost(int UserId, string Text);
    }
}