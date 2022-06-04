using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategorys()
        {
            try
            {
                return await _categoryService.ListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
        {
            try
            {
                CategoryDTO category = await _categoryService.GetById(id);

                if (category is null)
                    return NotFound();

                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> PutCategory(Guid id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
                return BadRequest();

            try
            {
                var category = new Category(id, new Title(categoryDTO.Title));
                await _categoryService.Update(category);
                CategoryDTO updatedCategory = await _categoryService.GetById(id);

                if (updatedCategory is null)
                    return NotFound();

                return updatedCategory;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Faild to update category with Id {id}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(NewCategoryDTO categoryDTO)
        {
            var category = new Category(new Title(categoryDTO.Title));

            try
            {
                await _categoryService.Add(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return UnprocessableEntity(category);
            }

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> DeleteCategory(Guid id)
        {
            var category = await _categoryService.GetById(id);

            if (category is null)
                return NotFound();

            try
            {
                await _categoryService.Remove(category);
                CategoryDTO deletedCategory = category;
                return deletedCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Can't delete category with Id {id}");
            }
        }
    }
}
