using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IBabaCapacitiesService
    {
        Task<BabaCapacity> AddBabaCapacityAsync(BabaCapacity baba);
        Task<BabaCapacity> UpdateBabaCapacityAsync(BabaCapacity baba, int id);
        Task<bool> DeleteBabaCapacityByIdAsync(int id);
        Task<bool> DeleteAllBabaCapacitiesAsync();
        Task<BabaCapacity> GetBabaCapacityByIdAsync(int id);
        Task<IEnumerable<BabaCapacity>> GetAllBabaCapacitiesAsync();
    }
}