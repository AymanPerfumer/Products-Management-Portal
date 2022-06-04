using Domain.Entities;

namespace Application.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public static implicit operator CategoryDTO(Category category)
        {
            if (category is null)
                return null;

            return new CategoryDTO
            {
                Id = category.Id,
                Title = category.Title.Value
            };
        }

        //public static implicit operator Category(CategoryDTO category)
        //{
        //    if (category is null)
        //        return null;

        //    return new Category
        //    {
        //        Id = category.Id,
        //        Name = category.Name
        //    };
        //}
    }
}
