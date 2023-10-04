using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class BabaProvideSService : IBabaProvideSService
    {
        private readonly IBabaProvideServicesRepository _babaProvideServicesRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IBabaRepository _babaRepository;
        private readonly IKindNanniesRepository _kindNanniesRepository;

        public BabaProvideSService(IBaseRepository baseRepository, IBabaProvideServicesRepository babaProvideServicesRepository, IBabaRepository babaRepository, IKindNanniesRepository kindNanniesRepository)
        {
            _baseRepository = baseRepository;
            _babaProvideServicesRepository = babaProvideServicesRepository;
            _babaRepository = babaRepository;
            _kindNanniesRepository = kindNanniesRepository;
        }

        public async Task<BabaProvideService> AddBabaProvideServiceAsync(BabaProvideService babaProvideService)
        {
            try
            {
                var baba = await _babaRepository.GetBabaByIdAsync(babaProvideService.BabaId)
                ??
                throw new Exception("BabaProvideService.BabaId does not persist on the database");

                var kindNanny = await _kindNanniesRepository.GetKindNannyByIdAsync(babaProvideService.KindNannyId)
                ??
                throw new Exception("BabaProvideService.KindNannyId does not persist on the database");

                var alreadyExist = await _babaProvideServicesRepository.CheckAlreadyExist(babaProvideService.BabaId, babaProvideService.KindNannyId);
                if (alreadyExist != null) throw new Exception("BabaProvideServices already exist on the database.");

                _baseRepository.Add(babaProvideService);

                if (await _baseRepository.SaveChangesAsync())
                    return await _babaProvideServicesRepository.GetBabaProvideServiceByIdAsync(babaProvideService.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllBabaProvideServicesAsync()
        {
            var babaProvideServices = await _babaProvideServicesRepository.GetAllBabaProvideServicesAsync();
            if (!babaProvideServices.Any()) throw new Exception("There aren't any BabaProvideServices on the database.");

            _baseRepository.DeleteRange(babaProvideServices.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteBabaProvideServiceAsync(int id)
        {
            try
            {
                var babaProvideServiceFromDB = await _babaProvideServicesRepository.GetBabaProvideServiceByIdAsync(id)
                ??
                throw new Exception("BabaProvideService.Id does not persist on the database.");

                _baseRepository.Delete(babaProvideServiceFromDB);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BabaProvideService>> GetAllBabaProvideServicesAsync()
        {
            try
            {
                var babaProvideServices = await _babaProvideServicesRepository.GetAllBabaProvideServicesAsync();
                if (babaProvideServices == null) return null;

                return babaProvideServices;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BabaProvideService> GetBabaProvideServiceByIdAsync(int id)
        {
            var babaProvideService = await _babaProvideServicesRepository.GetBabaProvideServiceByIdAsync(id);
            if (babaProvideService == null) return null;

            return babaProvideService;
        }

        public async Task<BabaProvideService> UpdateBabaProvideServiceAsync(BabaProvideService babaProvideService, int id)
        {
            try
            {
                var babaProvideServiceFromDB = await _babaProvideServicesRepository.GetBabaProvideServiceByIdAsync(id)
                ??
                throw new Exception("BabaProvideService.Id does not persist on the database.");

                babaProvideService.Id = babaProvideServiceFromDB.Id;

                _baseRepository.Update(babaProvideService);

                if (await _baseRepository.SaveChangesAsync())
                    return await _babaProvideServicesRepository.GetBabaProvideServiceByIdAsync(babaProvideService.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}