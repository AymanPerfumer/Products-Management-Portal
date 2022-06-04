using Domain.Entities;

namespace Application.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }


        public static implicit operator ProductDTO(Product product)
        {
            if (product is null)
                return null;

            return new ProductDTO
            {
                Id = product.Id,
                Title = product.Title.Value,
                Description = product.Description?.Value,
                Category = product.Category?.Title.Value,
                Price = product.Price.Value,
                Image = product.Image?.Url
            };
        }
    }
}
