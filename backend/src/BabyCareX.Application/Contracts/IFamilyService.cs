using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IFamilyService
    {
        Task<Family> AddFamilyAsync(Family family);
        Task<Family> UpdateFamilyAsync(Family family, int id);
        Task<bool> DeleteFamilyByIdAsync(int id);
        Task<bool> DeleteAllFamiliesAsync();
        Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password);
        Task<Family> GetFamilyByIdAsync(int id);
        Task<IEnumerable<Family>> GetAllFamiliesAsync();
    }
}