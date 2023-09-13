using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;

namespace BabyCareX.Repository.Contracts
{
    public interface IBaseRepository
    {
        void Add<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        Task<bool> ChangeStatus<T>(T entity, EStatus status) where T : BaseEntity;
        Task<bool> SaveChangesAsync();
    }
}