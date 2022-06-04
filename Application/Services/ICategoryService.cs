using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICategoryService : ICrudService<Category>
    {
        Task<List<CategoryDTO>> ListAsync();
    }
}
