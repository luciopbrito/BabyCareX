using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class FamilyService : IFamilyService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IFamilyRepository _familyRepository;

        public FamilyService(IBaseRepository baseRepository, IFamilyRepository familyRepository)
        {
            _baseRepository = baseRepository;
            _familyRepository = familyRepository;
        }

        public async Task<Family> AddFamily(Family familyEntity)
        {
            try
            {
                _baseRepository.Add(familyEntity);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _familyRepository.GetFamilyById(familyEntity.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFamily(int id)
        {
            try
            {
                var family = await _familyRepository.GetFamilyById(id) ?? throw new Exception("Family to delete does not found.");

                _baseRepository.Delete(family);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password)
        {
            return await _familyRepository.GetFamilyByEmailAndPasswordAsync(email, password);
        }

        public async Task<Family> GetFamilyById(int id)
        {
            return await _familyRepository.GetFamilyById(id);
        }

        public async Task<Family> UpdateFamily(Family family, int id)
        {
            try
            {
                var familyFromDb = _familyRepository.GetFamilyById(id)
                ??
                throw new Exception("Family to update does not found.");

                family.Id = familyFromDb.Id;

                _baseRepository.Update(family);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _familyRepository.GetFamilyById(family.Id);
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