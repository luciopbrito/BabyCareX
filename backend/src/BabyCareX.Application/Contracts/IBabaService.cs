using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IBabaService
    {
        Task<Baba> AddBabaAsync(Baba baba);
        Task<Baba> UpdateBabaAsync(Baba baba, int id);
        Task<bool> DeleteBabaByIdAsync(int id);
        Task<bool> DeleteAllBabasAsync();
        Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password);
        Task<Baba> GetBabaByIdAsync(int id);
        Task<IEnumerable<Baba>> GetAllBabasAsync();
    }
}