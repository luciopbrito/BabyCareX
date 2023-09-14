using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application.Contracts
{
    public interface IFamilyService
    {
        Task<Family> AddFamily(Family family);
        Task<Family> UpdateFamily(Family family, int id);
        Task<bool> DeleteFamily(int id);
        Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password);
        Task<Family> GetFamilyById(int id);
    }
}