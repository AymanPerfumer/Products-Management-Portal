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
    public class CategoryRepository : RepositoryBase<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext, ILogger<CategoryRepository> logger) : 
            base(appDbContext, logger)
        {
        }
    }
}
