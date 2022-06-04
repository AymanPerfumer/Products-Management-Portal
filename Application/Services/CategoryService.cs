
using Application.DTOs;
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
    public class CategoryService : CrudService<Category>, ICategoryService
    {
        public CategoryService(ICategoryRepository repository) 
            : base(repository)
        {
        }

        public Task<List<CategoryDTO>> ListAsync()
        {
            return ListAsync(c => true, c => (CategoryDTO)c);
        }
    }
}
