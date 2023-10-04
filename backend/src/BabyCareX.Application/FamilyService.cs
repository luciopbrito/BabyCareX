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

        public async Task<Family> AddFamilyAsync(Family family)
        {
            try
            {
                var alreadyExist = await _familyRepository.CheckIfAlreadyRegistered(family.Email);

                if (alreadyExist != null)
                    throw new Exception("Email already persists on the database.");

                if (family.Children != null)
                {
                    foreach (var f in family.Children)
                    {
                        f.CreatedAt = DateTime.Now;
                    }
                }

                if (family.Schedules != null)
                {
                    foreach (var f in family.Schedules)
                    {
                        f.CreatedAt = DateTime.Now;
                    }
                }

                _baseRepository.Add(family);

                if (await _baseRepository.SaveChangesAsync())
                    return await _familyRepository.GetFamilyByIdAsync(family.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllFamiliesAsync()
        {
            var families = await _familyRepository.GetAllFamiliesAsync();
            if (!families.Any()) throw new Exception("There aren't any Families on the database.");

            _baseRepository.DeleteRange(families.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteFamilyByIdAsync(int id)
        {
            try
            {
                var family = await _familyRepository.GetFamilyByIdAsync(id) ?? throw new Exception("Family to delete does not found.");

                _baseRepository.Delete(family);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Family>> GetAllFamiliesAsync()
        {
            var families = await _familyRepository.GetAllFamiliesAsync();
            if (families == null) return null;

            return families;
        }

        public async Task<Family> GetFamilyByEmailAndPasswordAsync(string email, string password)
        {
            var family = await _familyRepository.GetFamilyByEmailAndPasswordAsync(email, password);
            if (family == null) return null;

            return family;
        }

        public async Task<Family> GetFamilyByIdAsync(int id)
        {
            var family = await _familyRepository.GetFamilyByIdAsync(id);
            if (family == null) return null;

            return family;
        }

        public async Task<Family> UpdateFamilyAsync(Family family, int id)
        {
            try
            {
                var familyFromDb = await _familyRepository.GetFamilyByIdAsync(id)
                ??
                throw new Exception("Family to update does not found.");

                family.Id = familyFromDb.Id;

                _baseRepository.Update(family);

                if (await _baseRepository.SaveChangesAsync())
                {
                    return await _familyRepository.GetFamilyByIdAsync(family.Id);
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