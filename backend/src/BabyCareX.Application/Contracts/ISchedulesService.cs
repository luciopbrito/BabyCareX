using BabyCareX.Domain.Entities;

namespace BabyCareX.Application.Contracts
{
    public interface ISchedulesService
    {
        Task<Schedule> AddScheduleAsync(Schedule scheduleToSave);
        Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate, int id);
        Task<bool> DeleteScheduleByIdAsync(int id);
        Task<bool> DeleteAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<IEnumerable<Schedule>> GetAllSchedulesByFamilyId(int familyId);
        Task<IEnumerable<Schedule>> GetAllSchedulesByBabaId(int babaId);
    }
}