using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IBabaRepository
    {
        Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password);
        Task<Baba> GetBabaById(int id);
    }
}