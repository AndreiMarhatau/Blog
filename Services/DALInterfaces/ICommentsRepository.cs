using DomainModels;
using System.Threading.Tasks;

namespace DALInterfaces
{
    public interface ICommentsRepository
    {
        Task AddComment(Comment post);
    }
}
