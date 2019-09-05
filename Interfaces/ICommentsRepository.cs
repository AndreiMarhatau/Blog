using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICommentsRepository
    {
        Task AddComment(Comment post);
    }
}
