﻿using DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ILikeRepository
    {
        Task AddOrRemoveLike(Guid userId, Post post);
        Task AddOrRemoveLike(Guid userId, Comment comment);
    }
}
