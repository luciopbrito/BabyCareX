using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IFamilyRepository
    {
        Task<IEnumerable<Family>> GetAllFamiliesAsync();
        Task<Family> GetFamilyByIdAsync(int id);
        Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password);
        Task<Family> CheckIfAlreadyRegistered(string email);
    }
}