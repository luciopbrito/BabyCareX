using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly BabyCareXContext _context;

        public StatusRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Status>> GetAllStatusAsync()
        {
            IQueryable<Status> query = _context.Status;

            query = query.OrderBy(s => s.Id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            IQueryable<Status> query = _context.Status;

            query = query.OrderBy(s => s.Id)
                        .Where(s => s.Id == id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}