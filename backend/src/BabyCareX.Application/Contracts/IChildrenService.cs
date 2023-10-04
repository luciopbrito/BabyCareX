using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IChildrenService
    {
        Task<Child> AddChildAsync(Child child);
        Task<Child> UpdateChildAsync(Child child, int id);
        Task<bool> DeleteChildByIdAsync(int id);
        Task<bool> DeleteAllChildrenAsync();
        Task<Child> GetChildByIdAsync(int id);
        Task<IEnumerable<Child>> GetAllChildrenAsync();
    }
}