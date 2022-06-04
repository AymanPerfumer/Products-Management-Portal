using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        public ProductController(IProductService productService,
            IWebHostEnvironment hostingEnvironment, ILogger<ProductController> logger)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                return await _productService.ListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            try
            {
                ProductDTO product = await _productService.GetById(id);

                if (product is null)
                    return NotFound();

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> PutProduct(Guid id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
                return BadRequest();

            try
            {
                var product = new Product(id, new Title(productDTO.Title))
                {
                    Description = string.IsNullOrEmpty(productDTO.Description) ? null : new Description(productDTO.Description),
                    Price = new Money(productDTO.Price),
                    Image = string.IsNullOrEmpty(productDTO.Image) ? null : new Image(productDTO.Image)
                };
                await _productService.Update(product);
                ProductDTO updatedProduct = await _productService.GetById(id);

                if (updatedProduct is null)
                    return NotFound();

                return updatedProduct;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Faild to update product with Id {id}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(NewProductDTO productDTO)
        {
            ProductDTO addedProduct;

            try
            {
                addedProduct = await _productService.Add(productDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return UnprocessableEntity(productDTO);
            }

            return CreatedAtAction("GetProduct", new { id = addedProduct.Id }, addedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(Guid id)
        {
            var product = await _productService.GetById(id);

            if (product is null)
                return NotFound();

            try
            {
                await _productService.Remove(product);
                ProductDTO deletedProduct = product;
                return deletedProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Can't delete product with Id {id}");
            }
        }

        [HttpGet("ByTitle")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return BadRequest();
            try
            {
                return await _productService.ListAsync(p => p.Title == new Title(title));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Internal server error");
            }
        }

        [HttpGet("ByDescription")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                return BadRequest();

            try
            {
                return await _productService.ListAsync(p => p.Description == new Description(description));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Problem($"Internal server error");
            }
        }

        [HttpPost("SaveFile")]
        public async Task<IActionResult> SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var postedFile = httpRequest.Form.Files[0];
                string filename = Guid.NewGuid().ToString() + postedFile.FileName;
                var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, "Photos");
                Directory.CreateDirectory(physicalPath);

                using (Stream fileStream = new FileStream(physicalPath +"/"+ filename, FileMode.Create))
                {
                    await postedFile.CopyToAsync(fileStream);
                    return new JsonResult(new { filename = filename});
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { filename = "anonymous.png" });
            }
        }
    }
}
