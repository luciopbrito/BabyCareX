using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class BabaCapacitiesService : IBabaCapacitiesService
    {
        private readonly IBabaCapacitiesRepository _babaCapacitiesRepository;
        private readonly IBabaRepository _babaRepository;
        private readonly IBaseRepository _baseRepository;

        public BabaCapacitiesService(
            IBaseRepository baseRepository,
            IBabaCapacitiesRepository babaCapacitiesRepository,
            IBabaRepository babaRepository
        )
        {
            _baseRepository = baseRepository;
            _babaCapacitiesRepository = babaCapacitiesRepository;
            _babaRepository = babaRepository;
        }

        public async Task<BabaCapacity> AddBabaCapacityAsync(BabaCapacity babaCapacity)
        {
            try
            {
                var alreadyPersist = await _babaRepository.GetBabaByIdAsync(babaCapacity.BabaId)
                ??
                throw new Exception("Baba.Id does not persist on the database.");

                _baseRepository.Add(babaCapacity);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaCapacitiesRepository.GetBabaCapacityByIdAsync(babaCapacity.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllBabaCapacitiesAsync()
        {
            var babaCapacities = await _babaCapacitiesRepository.GetAllBabaCapacitiesAsync();
            if (!babaCapacities.Any()) throw new Exception("There aren't any BabaCapacities on the database.");

            _baseRepository.DeleteRange(babaCapacities.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteBabaCapacityByIdAsync(int id)
        {
            try
            {
                var babaCapatity = await _babaCapacitiesRepository.GetBabaCapacityByIdAsync(id)
                ??
                throw new Exception("BabaCapacity.Id does not persist on the database.");

                _baseRepository.Delete(babaCapatity);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BabaCapacity>> GetAllBabaCapacitiesAsync()
        {
            var babaCapacities = await _babaCapacitiesRepository.GetAllBabaCapacitiesAsync();
            if (babaCapacities == null) return null;

            return babaCapacities;
        }

        public async Task<BabaCapacity> GetBabaCapacityByIdAsync(int id)
        {
            var babaCapacity = await _babaCapacitiesRepository.GetBabaCapacityByIdAsync(id);
            if (babaCapacity == null) return null;

            return babaCapacity;
        }

        public async Task<BabaCapacity> UpdateBabaCapacityAsync(BabaCapacity babaCapacity, int id)
        {
            try
            {
                var baba = await _babaRepository.GetBabaByIdAsync(babaCapacity.BabaId)
                ??
                throw new Exception("BabaCapacity.BabaId does not persist on the database.");

                var alreadyPersist = await _babaCapacitiesRepository.GetBabaCapacityByIdAsync(id)
                ??
                throw new Exception("BabaCapacity.Id does not persist on the database.");

                babaCapacity.Id = alreadyPersist.Id;

                _baseRepository.Update(babaCapacity);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaCapacitiesRepository.GetBabaCapacityByIdAsync(babaCapacity.Id);
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