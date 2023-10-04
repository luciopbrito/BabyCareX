using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class KindNanniesService : IKindNanniesService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IKindNanniesRepository _kindNanniesRepository;

        public KindNanniesService(IBaseRepository baseRepository, IKindNanniesRepository kindNanniesRepository)
        {
            _baseRepository = baseRepository;
            _kindNanniesRepository = kindNanniesRepository;
        }

        public async Task<KindNanny> AddKindNannyAsync(KindNanny kindNanny)
        {
            try
            {
                _baseRepository.Add(kindNanny);

                if (await _baseRepository.SaveChangesAsync())
                    return await _kindNanniesRepository.GetKindNannyByIdAsync(kindNanny.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllKindNanniesAsync()
        {
            var kindNannies = await _kindNanniesRepository.GetAllKindNanniesAsync();
            if (!kindNannies.Any()) throw new Exception("There aren't any KindNannies on the database.");

            _baseRepository.DeleteRange(kindNannies.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteKindNannyByIdAsync(int id)
        {
            try
            {
                var kindNanny = await _kindNanniesRepository.GetKindNannyByIdAsync(id)
                ??
                throw new Exception("KindNanny.Id does not persist on the database.");

                _baseRepository.Delete(kindNanny);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<KindNanny>> GetAllKindNanniesAsync()
        {
            try
            {
                var kindNannies = await _kindNanniesRepository.GetAllKindNanniesAsync();
                if (kindNannies == null) return null;

                return kindNannies;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<KindNanny> GetKindNannyByIdAsync(int id)
        {
            try
            {
                var kindNanny = await _kindNanniesRepository.GetKindNannyByIdAsync(id);
                if (kindNanny == null) return null;

                return kindNanny;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<KindNanny> UpdateKindNannyAsync(KindNanny kindNanny, int id)
        {
            try
            {
                var kindNannyFromDB = await _kindNanniesRepository.GetKindNannyByIdAsync(id)
                ??
                throw new Exception("KindNanny.Id does not persist on the database.");

                kindNanny.Id = kindNannyFromDB.Id;

                _baseRepository.Update(kindNanny);

                if (await _baseRepository.SaveChangesAsync())
                    return await _kindNanniesRepository.GetKindNannyByIdAsync(kindNanny.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}