using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application.Contracts
{
    public interface IBabaService
    {
        Task<Baba> AddBaba(Baba baba);
        Task<Baba> UpdateBaba(Baba baba, int id);
        Task<bool> DeleteBaba(int id);
        Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password);
        Task<Baba> GetBabaById(int id);
    }
}