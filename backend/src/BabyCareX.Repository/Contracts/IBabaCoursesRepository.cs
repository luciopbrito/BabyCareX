using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface IBabaCoursesRepository
    {
        Task<BabaCourse> GetBabaCourseByIdAsync(int id);
        Task<IEnumerable<BabaCourse>> GetAllBabaCoursesAsync();
    }
}