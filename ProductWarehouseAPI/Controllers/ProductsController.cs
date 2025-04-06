using Microsoft.AspNetCore.Mvc;
using ProductWarehouse.Service.DTO;
using ProductWarehouse.Service.Interfaces;

namespace ProductWarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
        {
            var products = await _productService.GetProductByIdAsync(id);

            if (products == null)
                return NotFound(new { message = "Product not found" });

            return products;
        }

        [HttpPost]
        public async Task<IActionResult> AddProducts(ProductCreateDto productsDto)
        {
            var result = await _productService.AddProductsAsync(productsDto);
            if (!result)
                return NotFound(new { message = "Product not found" });

            return Ok(new { message = "Product created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, [FromBody] ProductUpdateDto products)
        {
            var result = await _productService.UpdateProductAsync(id, products);
            if (!result)
                return NotFound(new { message = "Product not found" });

            return Ok(new { message = "Product updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Product not found" });

            return Ok(new { message = "Product deleted successfully." });
        }

        [HttpPut("/decrement-stock/{id:int}/{quantity:int}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            await _productService.DecrementStockAsync(id, quantity);
            return Ok(new { message = "Stock decremented successfully." });
        }

        [HttpPut("/add-to-stock/{id:int}/{quantity:int}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            await _productService.IncrementStockAsync(id, quantity);
            return Ok(new { message = "Stock added successfully." });
        }
    }
}
