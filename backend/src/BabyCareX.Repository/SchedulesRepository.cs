using BabyCareX.Application.Models;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BabyCareX.Repository
{
    public class SchedulesRepository : ISchedulesRepository
    {
        private readonly BabyCareXContext _context;

        public SchedulesRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public async Task<Schedule> CheckAlreadyExist(int babaId, int familyId)
        {
            IQueryable<Schedule> query = _context.Schedules
             .Include(s => s.Baba)
             .Include(s => s.Family);

            query = query.Where(s => s.BabaId == babaId).Where(s => s.FamilyId == familyId).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TimeAWeekHandle> CheckIsMaximumTimesAWeekBabaId(int babaId, int qtdToAdd)
        {
            IQueryable<Schedule> query = _context.Schedules
             .Include(s => s.Baba)
             .Include(s => s.Family).AsNoTracking();

            query = query.Where(s => s.BabaId == babaId);

            var totalTimesAWeek = await query.SumAsync(s => s.TimesAWeek);

            int timesAWeekRemaining = totalTimesAWeek == 7 ?
                timesAWeekRemaining = totalTimesAWeek :
                (totalTimesAWeek + qtdToAdd) - 7;

            return await Task.FromResult(new TimeAWeekHandle()
            {
                Status = (totalTimesAWeek + qtdToAdd) > 7 ? false : true,
                TimeAWeekRemaining = timesAWeekRemaining
            });
        }

        public async Task<TimeAWeekHandle> CheckIsMaximumTimesAWeekFamilyId(int familyId, int qtdToAdd)
        {
            IQueryable<Schedule> query = _context.Schedules
           .Include(s => s.Baba)
           .Include(s => s.Family).AsNoTracking();

            query = query.Where(s => s.FamilyId == familyId);

            var totalTimesAWeek = await query.SumAsync(s => s.TimesAWeek);

            int timesAWeekRemaining = totalTimesAWeek == 7 ?
            timesAWeekRemaining = totalTimesAWeek :
            (totalTimesAWeek + qtdToAdd) - 7;

            return await Task.FromResult(new TimeAWeekHandle()
            {
                Status = (totalTimesAWeek + qtdToAdd) > 7 ? false : true,
                TimeAWeekRemaining = timesAWeekRemaining
            });
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            IQueryable<Schedule> query = _context.Schedules
                .Include(s => s.Baba)
                .Include(s => s.Family);

            query = query.OrderBy(s => s.Id).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByBabaId(int babaId)
        {
            IQueryable<Schedule> query = _context.Schedules
                .Include(s => s.Baba)
                .Include(s => s.Family);

            query = query.OrderBy(s => s.Id)
                        .Where(s => s.BabaId == babaId).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByFamilyId(int familyId)
        {
            IQueryable<Schedule> query = _context.Schedules
                .Include(s => s.Baba)
                .Include(s => s.Family);

            query = query.OrderBy(s => s.Id)
                        .Where(s => s.FamilyId == familyId).AsNoTracking();

            return await query.ToArrayAsync();
        }

        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            IQueryable<Schedule> query = _context.Schedules
                .Include(s => s.Baba)
                .Include(s => s.Family);

            query = query.OrderBy(s => s.Id)
                        .Where(s => s.Id == id).AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }
    }
}