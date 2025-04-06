using ProductWarehouse.Service.DTO;

namespace ProductWarehouse.Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductReadDto>> GetAllProductsAsync();
        Task<ProductReadDto> GetProductByIdAsync(int productId);
        Task<bool> AddProductsAsync(ProductCreateDto productsDto);
        Task<bool> UpdateProductAsync(int id, ProductUpdateDto dto);
        Task<bool> DeleteAsync(int productId);
        Task DecrementStockAsync(int productId, int quantity);
        Task IncrementStockAsync(int productId, int quantity);
    }
}
