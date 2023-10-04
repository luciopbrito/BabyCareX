using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface IBabaCoursesService
    {
        Task<BabaCourse> AddBabaCourseAsync(BabaCourse babaCourse);
        Task<BabaCourse> UpdateBabaCourseAsync(BabaCourse babaCourse, int id);
        Task<bool> DeleteBabaCourseAsync(int id);
        Task<bool> DeleteAllBabaCoursesAsync();
        Task<BabaCourse> GetBabaCourseByIdAsync(int id);
        Task<IEnumerable<BabaCourse>> GetAllBabaCoursesAsync();

    }
}