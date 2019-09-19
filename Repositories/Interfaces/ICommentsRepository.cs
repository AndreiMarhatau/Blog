using System.Threading.Tasks;
using DomainModels;

namespace Interfaces
{
    public interface ICommentsRepository
    {
        Task AddComment(Comment post);
    }
}
