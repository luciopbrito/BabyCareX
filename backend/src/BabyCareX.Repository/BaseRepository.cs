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
            entity.CreatedAt = DateTime.Now;
            _context.Add(entity);
        }

        public async Task<bool> ChangeStatus<T>(T entity, EStatus status) where T : BaseEntity
        {
            switch (status)
            {
                case EStatus.ATIVO:
                    entity.StatusId = 1;
                    _context.Update(entity);
                    break;
                case EStatus.INATIVO:
                    entity.StatusId = 2;
                    _context.Update(entity);
                    break;
                case EStatus.CANCELADO:
                    entity.StatusId = 3;
                    _context.Update(entity);
                    break;
                case EStatus.DELETADO:
                    entity.StatusId = 4;
                    _context.Update(entity);
                    break;
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _context.Remove(entity);
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