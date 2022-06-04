using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Services
{
    public interface ICrudService<TEntity> where TEntity : IEntity<Guid>
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expr);

        Task<List<TResult>> ListAsync<TResult>(Expression<Func<TEntity, bool>> expr,
            Expression<Func<TEntity, TResult>> selector);

        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Remove(TEntity entity);
        Task<bool> CanRemove(TEntity entity);
        Task Update(TEntity entity);

        Task<TEntity> GetById(Guid id);
        Task BeginTransaction();
        void CommitChanges();
        void RollbackChanges();
    }
}