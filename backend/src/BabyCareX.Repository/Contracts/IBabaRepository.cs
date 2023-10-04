using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IBabaRepository
    {
        Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password);
        Task<Baba> GetBabaByIdAsync(int id);
        Task<IEnumerable<Baba>> GetAllBabasAsync();
        Task<Baba> CheckIfAlreadyRegistered(string email);
    }
}