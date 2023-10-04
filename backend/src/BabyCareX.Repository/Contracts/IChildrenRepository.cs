using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IChildrenRepository
    {
        Task<Child> GetChildByIdAsync(int id);
        Task<IEnumerable<Child>> GetAllChildrenAsync();
    }
}