using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRepository<TEntity, TKey> : IDisposable
        where TEntity : AggregateRoot<TKey> where TKey : IEquatable<TKey>
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expr);

        Task<List<TResult>> ListAsync<TResult>(Expression<Func<TEntity, bool>> expr,
            Expression<Func<TEntity, TResult>> selector);

        TEntity? GetEntity(TKey id);
        Task<TEntity?> GetEntityAsync(TKey id);
        Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> spec);

        IQueryable<TEntity> GetAllEntities();
        Task<IList<TEntity>> GetAllEntitiesAsync();
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter);
        void Reload(TEntity entity);
        Task ReloadAsync(TEntity entity);

        void Insert(TEntity entity);
        bool IsEditable(TEntity entity);
        void Update(TEntity item);
        bool IsDeletable(TEntity entity);
        void Delete(TEntity item);

        Task<int> SaveChangesAsync();
        int SaveChanges();
        void RollBackChanges();
        void CommitChanges();
        void ClearTracker();
        Task BeginTransaction();
    }

    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : AggregateRoot<Guid>
    {
    }
}
