using BabyCareX.Application.Contracts;
using BabyCareX.Domain.Entities;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Application
{
    public class ChildrenService : IChildrenService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IChildrenRepository _childrenRepository;
        private readonly IFamilyRepository _familyRepository;

        public ChildrenService(IBaseRepository baseRepository, IChildrenRepository childrenRepository, IFamilyRepository familyRepository)
        {
            _baseRepository = baseRepository;
            _childrenRepository = childrenRepository;
            _familyRepository = familyRepository;
        }

        public async Task<Child> AddChildAsync(Child child)
        {
            try
            {
                var family = await _familyRepository.GetFamilyByIdAsync(child.FamilyId)
                ??
                throw new Exception("Child.FamilyId does not persist on the database.");

                _baseRepository.Add(child);

                if (await _baseRepository.SaveChangesAsync())
                    return await _childrenRepository.GetChildByIdAsync(child.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAllChildrenAsync()
        {
            var children = await _childrenRepository.GetAllChildrenAsync();
            if (!children.Any()) throw new Exception("There aren't any Children on the database.");

            _baseRepository.DeleteRange(children.ToArray());

            return await _baseRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteChildByIdAsync(int id)
        {
            try
            {
                var child = await _childrenRepository.GetChildByIdAsync(id)
                ??
                throw new Exception("Child.Id does not persist on the database");

                _baseRepository.Delete(child);

                return await _baseRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Child>> GetAllChildrenAsync()
        {
            try
            {
                var children = await _childrenRepository.GetAllChildrenAsync();
                if (children == null) return null;

                return children;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Child> GetChildByIdAsync(int id)
        {
            try
            {
                var child = await _childrenRepository.GetChildByIdAsync(id);
                if (child == null) return null;

                return child;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Child> UpdateChildAsync(Child child, int id)
        {
            try
            {
                var childFromDB = await _childrenRepository.GetChildByIdAsync(id)
                ??
                throw new Exception("Child.Id does not persist on the database");

                child.Id = childFromDB.Id;

                _baseRepository.Update(child);

                if (await _baseRepository.SaveChangesAsync())
                    return await _childrenRepository.GetChildByIdAsync(child.Id);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}