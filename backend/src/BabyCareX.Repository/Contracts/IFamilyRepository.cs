using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IFamilyRepository
    {
        Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password);
        Task<Family> GetFamilyById(int id);
    }
}