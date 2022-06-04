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
    public interface IProductService : ICrudService<Product>
    {
        Task<List<ProductDTO>> ListAsync();
        Task<List<ProductDTO>> ListAsync(Expression<Func<Product, bool>> expr);
        Task<ProductDTO> Add(NewProductDTO productDTO);
    }
}
