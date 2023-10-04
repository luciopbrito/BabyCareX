using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class BabaRepository : IBabaRepository
    {
        private readonly BabyCareXContext _context;

        public BabaRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<Baba> CheckIfAlreadyRegistered(string email)
        {
            IQueryable<Baba> query = _context.Babas
              .Include(b => b.BabaCapacities)
              .Include(b => b.BabaCourses)
              .Include(b => b.BabaProvideServices)
              .Include(b => b.Schedules).ThenInclude(s => s.Family);

            query = query.Where(b => b.Email == email).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Baba>> GetAllBabasAsync()
        {
            IQueryable<Baba> query = _context.Babas
                .Include(b => b.BabaCapacities)
                .Include(b => b.BabaCourses)
                .Include(b => b.BabaProvideServices)
                .ThenInclude(bps => bps.KindNanny)
                .Include(b => b.Schedules).ThenInclude(s => s.Family);

            query = query.OrderBy(b => b.Id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password)
        {
            IQueryable<Baba> query = _context.Babas
                .Include(b => b.BabaCapacities)
                .Include(b => b.BabaCourses)
                .Include(b => b.BabaProvideServices)
                .Include(b => b.Schedules).ThenInclude(s => s.Family);

            query = query.Where(e => e.Email == email && e.Password == password).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Baba> GetBabaByIdAsync(int id)
        {
            IQueryable<Baba> query = _context.Babas
                .Include(b => b.BabaCapacities)
                .Include(b => b.BabaCourses)
                .Include(b => b.BabaProvideServices)
                .Include(b => b.Schedules).ThenInclude(s => s.Family);

            query = query.Where(f => f.Id == id).OrderBy(f => f.Id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}