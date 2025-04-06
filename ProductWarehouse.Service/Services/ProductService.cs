using Microsoft.EntityFrameworkCore;
using ProductWarehouse.Enitity.Entities;
using ProductWarehouse.Infrastructure.Data;
using ProductWarehouse.Service.DTO;
using ProductWarehouse.Service.Interfaces;

namespace ProductWarehouse.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        public ProductService(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<ProductReadDto>> GetAllProductsAsync()
        {
            var products = await _context.products.ToListAsync();

            return products.Select(p => new ProductReadDto
            {
                ProductId = p.ProductId,
                ItemName = p.ItemName,
                BrandName = p.BrandName,
                Price = p.Price,
                TotalQuantity = p.TotalQuantity,
                Active = p.Active
            }).ToList();
        }

        public async Task<ProductReadDto> GetProductByIdAsync(int productId)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null) return null;

            return new ProductReadDto
            {
                ProductId = product.ProductId,
                ItemName = product.ItemName,
                BrandName = product.BrandName,
                Price = product.Price,
                TotalQuantity = product.TotalQuantity,
                Active = product.Active
            };
        }

        public async Task<bool> AddProductsAsync(ProductCreateDto productsDto)
        {
            var products = new Products
            {
                ItemName = productsDto.ItemName,
                BrandName = productsDto.BrandName,
                Price = productsDto.Price,
                TotalQuantity = productsDto.TotalQuantity,
                Active = productsDto.Active
            };

            _context.products.Add(products);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(int productId, ProductUpdateDto dto)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
                throw new Exception("Product not found");

            product.ItemName = dto.ItemName;
            product.BrandName = dto.BrandName;
            product.Price = dto.Price;
            product.Active = dto.Active;

            _context.products.Update(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null) return false;
            _context.products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DecrementStockAsync(int productId, int quantity)
        {
            string query = "UPDATE Products SET TotalQuantity = TotalQuantity - {0} WHERE ProductId = {1}";
            await _context.Database.ExecuteSqlRawAsync(query, quantity, productId);
        }

        public async Task IncrementStockAsync(int productId, int quantity)
        {
            string query = "UPDATE Products SET TotalQuantity = TotalQuantity + {0} WHERE ProductId = {1}";
            await _context.Database.ExecuteSqlRawAsync(query, quantity, productId);
        }
    }
}
