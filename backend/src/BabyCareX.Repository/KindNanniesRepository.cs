using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class KindNanniesRepository : IKindNanniesRepository
    {
        private readonly BabyCareXContext _context;

        public KindNanniesRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KindNanny>> GetAllKindNanniesAsync()
        {
            IQueryable<KindNanny> query = _context.KindNannies
                .Include(k => k.BabaProvideServices)
                    .ThenInclude(b => b.Baba);

            query = query.OrderBy(k => k.Id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<KindNanny> GetKindNannyByIdAsync(int id)
        {
            IQueryable<KindNanny> query = _context.KindNannies
               .Include(k => k.BabaProvideServices)
                   .ThenInclude(b => b.Baba);

            query = query.Where(k => k.Id == id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}