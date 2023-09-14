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

        public async Task<Baba> AddBaba(Baba baba)
        {
            try
            {
                _baseRepository.Add(baba);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaRepository.GetBabaById(baba.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBaba(int id)
        {
            try
            {
                var baba = await _babaRepository.GetBabaById(id)
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

        public async Task<Baba> GetBabaByEmailAndPasswordAsync(string email, string password)
        {
            return await _babaRepository.GetBabaByEmailAndPasswordAsync(email, password);
        }

        public async Task<Baba> GetBabaById(int id)
        {
            return await _babaRepository.GetBabaById(id);
        }

        public async Task<Baba> UpdateBaba(Baba baba, int id)
        {
            try
            {
                var babaFromDB = _babaRepository.GetBabaById(id)
               ??
               throw new Exception("Baba to update does not found");

                baba.Id = babaFromDB.Id;

                _baseRepository.Update(baba);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _babaRepository.GetBabaById(baba.Id);
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