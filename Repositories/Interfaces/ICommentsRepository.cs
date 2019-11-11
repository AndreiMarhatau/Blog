using DomainModels;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICommentsRepository
    {
        Task AddComment(Comment post);
    }
}
