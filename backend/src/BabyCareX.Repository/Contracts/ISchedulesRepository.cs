using BabyCareX.Application.Models;
using BabyCareX.Domain.Entities;

namespace BabyCareX.Repository.Contracts
{
    public interface ISchedulesRepository
    {
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<IEnumerable<Schedule>> GetAllSchedulesByFamilyId(int familyId);
        Task<IEnumerable<Schedule>> GetAllSchedulesByBabaId(int babaId);
        Task<TimeAWeekHandle> CheckIsMaximumTimesAWeekBabaId(int babaId, int qtdToAdd);
        Task<TimeAWeekHandle> CheckIsMaximumTimesAWeekFamilyId(int familyId, int qtdToAdd);
        Task<Schedule> CheckAlreadyExist(int babaId, int familyId);
    }
}