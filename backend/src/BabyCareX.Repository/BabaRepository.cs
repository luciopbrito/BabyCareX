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

        public async Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password)
        {
            IQueryable<Baba> query = _context.Babas
                .Include(b => b.BabaCapacities)
                .Include(b => b.BabaCourses)
                .Include(b => b.BabaProvideServices)
                .Include(b => b.Schedules).ThenInclude(s => s.Family);

            query.Where(e => e.Email == email && e.Password == password);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Baba> GetBabaById(int id)
        {
            IQueryable<Baba> query = _context.Babas
                .Include(b => b.BabaCapacities)
                .Include(b => b.BabaCourses)
                .Include(b => b.BabaProvideServices)
                .Include(b => b.Schedules).ThenInclude(s => s.Family);

            query.Where(f => f.Id == id).OrderBy(f => f.Id);

            return await query.FirstOrDefaultAsync();
        }

    }
}