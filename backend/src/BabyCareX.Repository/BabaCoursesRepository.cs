using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class BabaCoursesRepository : IBabaCoursesRepository
    {
        private readonly BabyCareXContext _context;

        public BabaCoursesRepository(BabyCareXContext babyCareXContext)
        {
            _context = babyCareXContext;
        }

        public async Task<IEnumerable<BabaCourse>> GetAllBabaCoursesAsync()
        {
            IQueryable<BabaCourse> query = _context.BabaCourses
                .Include(bcourse => bcourse.Baba).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<BabaCourse> GetBabaCourseByIdAsync(int id)
        {
            IQueryable<BabaCourse> query = _context.BabaCourses
                .Include(bcourse => bcourse.Baba);

            query = query.Where(bcourse => bcourse.Id == id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}