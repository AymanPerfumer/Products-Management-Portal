using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : AggregateRoot<TKey> where TKey : IEquatable<TKey>
    {
        protected AppDbContext _appDbContext;
        protected ILogger _logger;

        public RepositoryBase(AppDbContext appDbContext, ILogger logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expr)
        {
            var result = _appDbContext.Set<TEntity>().AnyAsync(expr);

            return result;
        }

        public async Task<List<TResult>> ListAsync<TResult>(Expression<Func<TEntity, bool>> expr,
            Expression<Func<TEntity, TResult>> selector)
        {
            var result = await GetAllEntities().Where(expr).Select(selector).ToListAsync();

            return result;
        }

        public virtual TEntity? GetEntity(TKey id)
        {
            try
            {
                return _appDbContext.Set<TEntity>().Find(id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public virtual async Task<TEntity?> GetEntityAsync(TKey id)
        {
            try
            {
                return await _appDbContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public virtual async Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> spec)
        {
            try
            {
                return await _appDbContext.Set<TEntity>().FirstOrDefaultAsync(spec);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public virtual IQueryable<TEntity> GetAllEntities()
        {
            return _appDbContext.Set<TEntity>();
        }

        public async Task<IList<TEntity>> GetAllEntitiesAsync()
        {
            return await GetAllEntities().ToListAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await GetAllEntities().CountAsync(filter);
        }

        public void Reload(TEntity entity)
        {
            try
            {
                _appDbContext.Entry(entity).Reload();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task ReloadAsync(TEntity entity)
        {
            try
            {
                await _appDbContext.Entry(entity).ReloadAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void Insert(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Remove(entity);
        }

        public virtual bool IsEditable(TEntity entity)
        {
            return true;
        }

        public virtual bool IsDeletable(TEntity entity)
        {
            return true;
        }

        public int SaveChanges()
        {
            try
            {
                return _appDbContext.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _appDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void RollBackChanges()
        {
            _appDbContext.Rollback();
        }

        public void CommitChanges()
        {
            _appDbContext.Commit();
        }

        public Task BeginTransaction()
        {
            return _appDbContext.BeginTransaction();
        }

        public void ClearTracker()
        {
            _appDbContext.ChangeTracker.Clear();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
