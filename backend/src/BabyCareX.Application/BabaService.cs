using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class BabaService : IBabaService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IBabaRepository _babaRepository;

        public BabaService(IBaseRepository baseRepository, IBabaRepository babaRepository)
        {
            _baseRepository = baseRepository;
            _babaRepository = babaRepository;
        }

        public async Task<Baba> AddBabaAsync(Baba baba)
        {
            try
            {
                var alreadyExist = await _babaRepository.CheckIfAlreadyRegistered(baba.Email);

                if (alreadyExist != null)
                    throw new Exception("Email already persists on the database.");

                if (baba.BabaCourses != null)
                {
                    foreach (var item in baba.BabaCourses)
                    {
                        item.CreatedAt = DateTime.Now;
                    }
                }

                if (baba.BabaCapacities != null)
                {
                    foreach (var item in baba.BabaCapacities)
                    {
                        item.CreatedAt = DateTime.Now;
                    }
                }

                if (baba.BabaProvideServices != null)
                {
                    foreach (var item in baba.BabaProvideServices)
                    {
                        item.CreatedAt = DateTime.Now;
                    }
                }

                if (baba.Schedules != null)
                {
                    foreach (var item in baba.Schedules)
                    {
                        item.CreatedAt = DateTime.Now;
                    }
                }

                _baseRepository.Add(baba);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaRepository.GetBabaByIdAsync(baba.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllBabasAsync()
        {
            var babas = await _babaRepository.GetAllBabasAsync();
            if (!babas.Any()) throw new Exception("There aren't any Babas on the database.");

            _baseRepository.DeleteRange(babas.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteBabaByIdAsync(int id)
        {
            try
            {
                var baba = await _babaRepository.GetBabaByIdAsync(id)
                ??
                throw new Exception("Baba to delete does not found");

                _baseRepository.Delete(baba);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Baba>> GetAllBabasAsync()
        {
            var babas = await _babaRepository.GetAllBabasAsync();
            if (babas == null) return null;

            return babas;
        }

        public async Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password)
        {
            var baba = await _babaRepository.GetBabaByEmailAndPasswordAsync(email, password);
            if (baba == null) return null;

            return baba;
        }

        public async Task<Baba> GetBabaByIdAsync(int id)
        {
            var baba = await _babaRepository.GetBabaByIdAsync(id);
            if (baba == null) return null;

            return baba;
        }

        public async Task<Baba> UpdateBabaAsync(Baba baba, int id)
        {
            try
            {
                var babaFromDB = await _babaRepository.GetBabaByIdAsync(id)
               ??
               throw new Exception("Baba to update does not found");

                baba.Id = babaFromDB.Id;

                _baseRepository.Update(baba);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaRepository.GetBabaByIdAsync(baba.Id);
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