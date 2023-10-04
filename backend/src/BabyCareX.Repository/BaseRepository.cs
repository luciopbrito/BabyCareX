using BabyCareX.Domain.Entities;
using BabyCareX.Domain.Enums;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;

namespace BabyCareX.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly BabyCareXContext _context;

        public BaseRepository(BabyCareXContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : BaseEntity
        {
            entity.StatusId = EStatus.ATIVO;
            entity.CreatedAt = DateTime.Now;
            _context.Add(entity);
        }

        public async Task<bool> ChangeStatus<T>(T entity, EStatus status) where T : BaseEntity
        {
            switch (status)
            {
                case EStatus.ATIVO:
                    entity.StatusId = EStatus.ATIVO;
                    _context.Update(entity);
                    break;
                case EStatus.INATIVO:
                    entity.StatusId = EStatus.INATIVO;
                    _context.Update(entity);
                    break;
                case EStatus.CANCELADO:
                    entity.StatusId = EStatus.CANCELADO;
                    _context.Update(entity);
                    break;
                case EStatus.DELETADO:
                    entity.StatusId = EStatus.DELETADO;
                    _context.Update(entity);
                    break;
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : BaseEntity
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Update(entity);
        }

    }
}