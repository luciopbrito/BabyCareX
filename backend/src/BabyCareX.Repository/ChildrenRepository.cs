using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class ChildrenRepository : IChildrenRepository
    {
        private readonly BabyCareXContext _context;

        public ChildrenRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Child>> GetAllChildrenAsync()
        {
            IQueryable<Child> query = _context.Children
                .Include(c => c.Family);

            query = query.OrderBy(c => c.Id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Child> GetChildByIdAsync(int id)
        {
            IQueryable<Child> query = _context.Children
                .Include(c => c.Family);

            query = query.Where(c => c.Id == id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}