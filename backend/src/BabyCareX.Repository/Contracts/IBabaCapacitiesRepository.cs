using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IBabaCapacitiesRepository
    {
        Task<BabaCapacity> GetBabaCapacityByIdAsync(int id);
        Task<IEnumerable<BabaCapacity>> GetAllBabaCapacitiesAsync();
        Task<IEnumerable<BabaCapacity>> GetAllBabaCapacitiesByBabaId(int id);
    }
}