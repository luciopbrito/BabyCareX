using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class BabaProvideServicesRepository : IBabaProvideServicesRepository
    {
        private readonly BabyCareXContext _context;

        public BabaProvideServicesRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<BabaProvideService> CheckAlreadyExist(int babaId, int kindNannyId)
        {
            IQueryable<BabaProvideService> query = _context.BabaProvideServices
                .Include(bps => bps.Baba)
                .Include(bps => bps.KindNanny);

            query = query.Where(bps => bps.BabaId == babaId && bps.KindNannyId == kindNannyId).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BabaProvideService>> GetAllBabaProvideServicesAsync()
        {
            IQueryable<BabaProvideService> query = _context.BabaProvideServices
                .Include(bps => bps.Baba)
                .Include(bps => bps.KindNanny);

            query = query.OrderBy(bps => bps.Id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<BabaProvideService> GetBabaProvideServiceByIdAsync(int id)
        {
            IQueryable<BabaProvideService> query = _context.BabaProvideServices
                .Include(bps => bps.Baba)
                .Include(bps => bps.KindNanny);

            query = query.Where(bps => bps.Id == id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}