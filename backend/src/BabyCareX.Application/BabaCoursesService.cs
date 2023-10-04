using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class BabaCoursesService : IBabaCoursesService
    {
        private readonly IBabaCoursesRepository _babaCoursesRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IBabaRepository _babaRepository;

        public BabaCoursesService(IBabaCoursesRepository babaCoursesRepository, IBaseRepository baseRepository, IBabaRepository babaRepository)
        {
            _babaCoursesRepository = babaCoursesRepository;
            _baseRepository = baseRepository;
            _babaRepository = babaRepository;
        }

        public async Task<BabaCourse> AddBabaCourseAsync(BabaCourse babaCourse)
        {
            try
            {
                var baba = await _babaRepository.GetBabaByIdAsync(babaCourse.BabaId)
                ??
                throw new Exception("Baba.Id does not persist on the database");

                if (babaCourse.IsCompleted && babaCourse.EndPeriod == null)
                    throw new Exception("BabaCourse.IsCompleted must be false.");

                _baseRepository.Add(babaCourse);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaCoursesRepository.GetBabaCourseByIdAsync(babaCourse.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllBabaCoursesAsync()
        {
            var babaCourses = await _babaCoursesRepository.GetAllBabaCoursesAsync();
            if (!babaCourses.Any()) throw new Exception("There aren't any BabaCourses on the database.");

            _baseRepository.DeleteRange(babaCourses.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteBabaCourseAsync(int id)
        {
            try
            {
                var babaCourse = await _babaCoursesRepository.GetBabaCourseByIdAsync(id)
                ??
                throw new Exception("BabaCourse.Id does not persist on the database.");

                _baseRepository.Delete(babaCourse);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BabaCourse>> GetAllBabaCoursesAsync()
        {
            try
            {
                var babaCourses = await _babaCoursesRepository.GetAllBabaCoursesAsync();
                if (babaCourses == null) return null;

                return babaCourses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BabaCourse> GetBabaCourseByIdAsync(int id)
        {
            try
            {
                var babaCourse = await _babaCoursesRepository.GetBabaCourseByIdAsync(id);
                if (babaCourse == null) return null;

                return babaCourse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BabaCourse> UpdateBabaCourseAsync(BabaCourse babaCourse, int id)
        {
            try
            {
                var babaCourseFromDB = await _babaCoursesRepository.GetBabaCourseByIdAsync(id)
                ??
                throw new Exception("BabaCourse.Id does not persist on the database.");

                babaCourse.Id = babaCourseFromDB.Id;

                _baseRepository.Update(babaCourse);
                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaCoursesRepository.GetBabaCourseByIdAsync(babaCourse.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}