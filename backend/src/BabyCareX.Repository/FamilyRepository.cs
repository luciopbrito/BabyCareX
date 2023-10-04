using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly BabyCareXContext _context;

        public FamilyRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<Family> CheckIfAlreadyRegistered(string email)
        {
            IQueryable<Family> query = _context.Families
                .Include(f => f.Children)
                .Include(f => f.Schedules).ThenInclude(s => s.Baba);

            query = query.Where(f => f.Email == email).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Family>> GetAllFamiliesAsync()
        {
            IQueryable<Family> query = _context.Families
                .Include(f => f.Children)
                .Include(f => f.Schedules).ThenInclude(s => s.Baba);

            query = query.OrderBy(f => f.Id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password)
        {
            IQueryable<Family> query = _context.Families
                .Include(f => f.Children)
                .Include(f => f.Schedules).ThenInclude(s => s.Baba);

            query = query.Where(e => e.Email == email && e.Password == password).OrderBy(e => e.Id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Family> GetFamilyByIdAsync(int id)
        {
            IQueryable<Family> query = _context.Families
                .Include(f => f.Children)
                .Include(f => f.Schedules);

            query = query.Where(f => f.Id == id).OrderBy(f => f.Id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

    }
}