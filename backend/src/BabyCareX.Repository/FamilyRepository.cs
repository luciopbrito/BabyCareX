using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;
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

        public async Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password)
        {
            IQueryable<Family> query = _context.Families
                .Include(f => f.Children)
                .Include(f => f.Schedules).ThenInclude(s => s.Baba);

            query.Where(e => e.Email == email && e.Password == password).OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Family> GetFamilyById(int id)
        {
            IQueryable<Family> query = _context.Families
                .Include(f => f.Children)
                .Include(f => f.Schedules);

            query.Where(f => f.Id == id).OrderBy(f => f.Id);

            return await query.FirstOrDefaultAsync();
        }

    }
}