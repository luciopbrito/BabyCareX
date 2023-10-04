using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IKindNanniesService
    {
        Task<KindNanny> AddKindNannyAsync(KindNanny kindNanny);
        Task<KindNanny> UpdateKindNannyAsync(KindNanny kindNanny, int id);
        Task<bool> DeleteKindNannyByIdAsync(int id);
        Task<bool> DeleteAllKindNanniesAsync();
        Task<KindNanny> GetKindNannyByIdAsync(int id);
        Task<IEnumerable<KindNanny>> GetAllKindNanniesAsync();
    }
}