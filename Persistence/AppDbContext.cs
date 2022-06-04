using Domain.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Persistence
{
    public class AppDbContext : IdentityDbContext, IUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task BeginTransaction()
        {
            _transaction?.Commit();
            _transaction = await Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new InvalidOperationException("There is no active transaction");
            try
            {
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
                throw new InvalidOperationException("There is no active transaction");

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }
    }
}
