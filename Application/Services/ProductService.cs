using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : CrudService<Product>, IProductService
    {
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, 
            ICategoryRepository categoryRepository)
            : base(repository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<List<ProductDTO>> ListAsync()
        {
            return ListAsync(c => true, c => (ProductDTO)c);
        }

        public Task<List<ProductDTO>> ListAsync(Expression<Func<Product, bool>> expr)
        {
            return ListAsync(expr, c => (ProductDTO)c);
        }

        public async Task<ProductDTO> Add(NewProductDTO productDTO)
        {
            var category = await _categoryRepository.GetEntityAsync(c => c.Title == new Title(productDTO.Category));

            if (category is null)
                return null;

            Product product = new Product(new Title(productDTO.Title))
            {
                Description = new Description(productDTO.Description),
                Category = category,
                Price = new Money(productDTO.Price),
                Image = new Image(productDTO.Image)
            };

            ProductDTO addedProduct = await base.Add(product);

            return addedProduct; ;    
        }
    }
}
