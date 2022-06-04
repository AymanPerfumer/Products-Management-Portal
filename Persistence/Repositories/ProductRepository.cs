using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product, Guid>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext, ILogger<ProductRepository> logger) :
            base(appDbContext, logger)
        {
        }

        public override IQueryable<Product> GetAllEntities()
        {
            return base.GetAllEntities().Include(p => p.Category);
        }
    }
}
