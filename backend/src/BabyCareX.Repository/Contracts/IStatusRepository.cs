using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IStatusRepository
    {
        Task<Status> GetStatusByIdAsync(int id);
        Task<IEnumerable<Status>> GetAllStatusAsync();
    }
}