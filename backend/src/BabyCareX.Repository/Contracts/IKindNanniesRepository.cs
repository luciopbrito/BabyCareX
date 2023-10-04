using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IKindNanniesRepository
    {
        Task<KindNanny> GetKindNannyByIdAsync(int id);
        Task<IEnumerable<KindNanny>> GetAllKindNanniesAsync();
    }
}