using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IBabaProvideServicesRepository
    {
        Task<BabaProvideService> GetBabaProvideServiceByIdAsync(int id);
        Task<IEnumerable<BabaProvideService>> GetAllBabaProvideServicesAsync();
        Task<BabaProvideService> CheckAlreadyExist(int babaId, int kindNannyId);

    }
}