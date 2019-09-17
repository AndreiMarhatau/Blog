using System.Threading.Tasks;
using Domain.Core;

namespace Interfaces
{
    public interface ICommentsRepository
    {
        Task AddComment(Comment post);
    }
}
