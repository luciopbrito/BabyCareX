using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class SchedulesService : ISchedulesService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly ISchedulesRepository _schedulesRepository;
        private readonly IBabaRepository _babaRepository;
        private readonly IFamilyRepository _familyRepository;

        public SchedulesService(ISchedulesRepository schedulesRepository, IBabaRepository babaRepository, IFamilyRepository familyRepository, IBaseRepository baseRepository)
        {
            _schedulesRepository = schedulesRepository;
            _babaRepository = babaRepository;
            _familyRepository = familyRepository;
            _baseRepository = baseRepository;
        }

        public async Task<Schedule> AddScheduleAsync(Schedule scheduleToSave)
        {
            try
            {
                var alreadyPersist = await _schedulesRepository.CheckAlreadyExist(scheduleToSave.BabaId, scheduleToSave.FamilyId);
                if (alreadyPersist != null) throw new Exception("Schedule already persist on the database");

                var baba = await _babaRepository.GetBabaByIdAsync(scheduleToSave.BabaId)
                ??
                throw new Exception("Schedule.BabaId does not persist on the database.");

                var family = await _familyRepository.GetFamilyByIdAsync(scheduleToSave.FamilyId)
                ??
                throw new Exception("Schedule.FamilyId does not persist on the database.");

                var isMaximumTimesAWeekFamily = await _schedulesRepository.CheckIsMaximumTimesAWeekFamilyId(scheduleToSave.FamilyId, scheduleToSave.TimesAWeek);

                if (isMaximumTimesAWeekFamily.TimeAWeekRemaining == 7)
                    throw new Exception("Cannot create a new Schedule because the free time the week of this family is over");

                if (!isMaximumTimesAWeekFamily.Status)
                    throw new Exception($"Can't create a new Schedule because the free time the week of this family must be less than {isMaximumTimesAWeekFamily.TimeAWeekRemaining}");

                var isMaximumTimesAWeekBaba = await _schedulesRepository.CheckIsMaximumTimesAWeekBabaId(scheduleToSave.BabaId, scheduleToSave.TimesAWeek);

                if (isMaximumTimesAWeekBaba.TimeAWeekRemaining == 7)
                    throw new Exception("Cannot create a new Schedule because the free time the week of this Baba is over");

                if (!isMaximumTimesAWeekBaba.Status)
                    throw new Exception($"Can't create a new Schedule because the free time the week of this Baba must be less than {isMaximumTimesAWeekBaba.TimeAWeekRemaining}");

                _baseRepository.Add(scheduleToSave);

                if (await _baseRepository.SaveChangesAsync())
                    return await _schedulesRepository.GetScheduleByIdAsync(scheduleToSave.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllSchedulesAsync()
        {
            var schedules = await _schedulesRepository.GetAllSchedulesAsync();
            if (!schedules.Any()) throw new Exception("There aren't any Schedules on the database.");

            _baseRepository.DeleteRange(schedules.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteScheduleByIdAsync(int id)
        {
            try
            {
                var schedule = await _schedulesRepository.GetScheduleByIdAsync(id)
                ??
                throw new Exception("Schedule.Id does not persist on the database.");

                _baseRepository.Delete(schedule);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            try
            {
                var schedules = await _schedulesRepository.GetAllSchedulesAsync();
                if (schedules == null) return null;

                return schedules;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByBabaId(int babaId)
        {
            try
            {
                var baba = await _babaRepository.GetBabaByIdAsync(babaId)
                ??
                throw new Exception("Baba.Id does not persist on the database.");

                var schedule = await _schedulesRepository.GetAllSchedulesByBabaId(babaId);
                if (schedule == null) return null;

                return schedule;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesByFamilyId(int familyId)
        {
            try
            {
                var family = await _familyRepository.GetFamilyByIdAsync(familyId)
                ??
                throw new Exception("Family.Id does not persist on the database.");

                var schedule = await _schedulesRepository.GetAllSchedulesByFamilyId(familyId);
                if (schedule == null) return null;

                return schedule;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Schedule> GetScheduleByIdAsync(int id)
        {
            try
            {
                var schedule = await _schedulesRepository.GetScheduleByIdAsync(id);
                if (schedule == null) return null;

                return schedule;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule scheduleToUpdate, int id)
        {
            try
            {
                var scheduleFromDB = await _schedulesRepository.GetScheduleByIdAsync(id)
                ??
                throw new Exception("Schedule.Id does not persist on the database.");

                scheduleToUpdate.Id = scheduleFromDB.Id;

                _baseRepository.Update(scheduleToUpdate);

                if (await _baseRepository.SaveChangesAsync())
                    return await _schedulesRepository.GetScheduleByIdAsync(scheduleToUpdate.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}