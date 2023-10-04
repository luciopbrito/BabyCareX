using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class BabaCapacitiesRepository : IBabaCapacitiesRepository
    {
        private readonly BabyCareXContext _context;

        public BabaCapacitiesRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BabaCapacity>> GetAllBabaCapacitiesAsync()
        {
            IQueryable<BabaCapacity> query = _context.BabaCapacities
                .Include(bc => bc.Baba).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<BabaCapacity>> GetAllBabaCapacitiesByBabaId(int id)
        {
            IQueryable<BabaCapacity> query = _context.BabaCapacities
                .Include(bc => bc.Baba);

            query = query.Where(bc => bc.BabaId == id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<BabaCapacity> GetBabaCapacityByIdAsync(int id)
        {
            IQueryable<BabaCapacity> query = _context.BabaCapacities
                .Include(bc => bc.Baba);

            query = query.Where(bc => bc.Id == id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}