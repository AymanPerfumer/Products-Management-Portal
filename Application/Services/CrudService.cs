using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CrudService<TEntity> : ICrudService<TEntity> where TEntity : AggregateRoot<Guid>
    {
        protected IRepository<TEntity> Repository;

        public CrudService(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expr)
        {
            var result = await Repository.AnyAsync(expr);

            return result;
        }

        public async Task<List<TResult>> ListAsync<TResult>(Expression<Func<TEntity, bool>> expr,
            Expression<Func<TEntity, TResult>> selector)
        {
            var result = await Repository.ListAsync(expr, selector);

            return result;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            Repository.Insert(entity);
            await Repository.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> Remove(TEntity entity)
        {
            var canRemove = await CanRemove(entity);
            if (!canRemove)
                return null;

            Repository.Delete(entity);
            await Repository.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> CanRemove(TEntity entity)
        {
            return true;
        }

        public virtual async Task Update(TEntity entity)
        {
            Repository.IsEditable(entity);
            Repository.Update(entity);
            await Repository.SaveChangesAsync();
        }

        public virtual async Task<TEntity?> GetById(Guid id)
        {
            return await Repository.GetEntityAsync(id);
        }

        public async Task<TEntity?> GetEntityAsync(Expression<Func<TEntity, bool>> spec)
        {
            return await Repository.GetEntityAsync(spec);
        }

        public async Task BeginTransaction()
        {
            await Repository.BeginTransaction();
        }

        public void CommitChanges()
        {
            Repository.CommitChanges();
        }

        public void RollbackChanges()
        {
            Repository.RollBackChanges();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Repository.GetCountAsync(filter);
        }
    }
}
