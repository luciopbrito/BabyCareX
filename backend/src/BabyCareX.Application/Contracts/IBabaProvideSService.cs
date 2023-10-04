using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IBabaProvideSService
    {
        Task<BabaProvideService> AddBabaProvideServiceAsync(BabaProvideService babaProvideService);
        Task<BabaProvideService> UpdateBabaProvideServiceAsync(BabaProvideService babaProvideService, int id);
        Task<bool> DeleteBabaProvideServiceAsync(int id);
        Task<bool> DeleteAllBabaProvideServicesAsync();
        Task<BabaProvideService> GetBabaProvideServiceByIdAsync(int id);
        Task<IEnumerable<BabaProvideService>> GetAllBabaProvideServicesAsync();

    }
}