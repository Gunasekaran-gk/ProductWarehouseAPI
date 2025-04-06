using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductWarehouse.Service.DTO;
using ProductWarehouse.Service.Interfaces;
using ProductWarehouseAPI.Controllers;

namespace ProductWarehouse.Tests;

[TestFixture]
public class ProductControllerTests
{
    private Mock<IProductService> _mockProductService;
    private ProductsController _controller;

    [SetUp]
    public void Setup()
    {
        _mockProductService = new Mock<IProductService>();
        _controller = new ProductsController(_mockProductService.Object);
    }

    [Test]
    public async Task GetAllProducts_ReturnsOk()
    {
        // Arrange
        var sampleProducts = new List<ProductReadDto>
    {
        new ProductReadDto { ProductId = 1, ItemName = "Laptop", Price = 1200 },
        new ProductReadDto { ProductId = 2, ItemName = "Phone", Price = 600 }
    };

        _mockProductService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(sampleProducts);

        // Act
        var result = await _controller.GetAllProducts();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var products = okResult.Value as List<ProductReadDto>;
        Assert.AreEqual(2, products?.Count);
        Assert.AreEqual("Laptop", products?[0].ItemName);
    }

    [Test]
    public async Task GetProductById_ProductExists_ReturnsOk()
    {
        var product = new ProductReadDto { ProductId = 1, ItemName = "Phone", Price = 100 };

        _mockProductService.Setup(x => x.GetProductByIdAsync(1)).ReturnsAsync(product);

        var result = await _controller.GetProductById(1);

        Assert.IsInstanceOf<ActionResult<ProductReadDto>>(result);
        Assert.AreEqual(product, result.Value);
    }

    [Test]
    public async Task GetProductById_ProductNotFound_ReturnsNotFound()
    {
        _mockProductService.Setup(x => x.GetProductByIdAsync(1)).ReturnsAsync((ProductReadDto)null);

        var result = await _controller.GetProductById(1);

        Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
    }

    [Test]
    public async Task AddProducts_WithValidData_ReturnsOk()
    {
        var newProductDto = new ProductCreateDto
        {
            ItemName = "Smartwatch",
            Price = 150,
            TotalQuantity = 20
        };

        _mockProductService
            .Setup(service => service.AddProductsAsync(newProductDto))
            .ReturnsAsync(true);

        var result = await _controller.AddProducts(newProductDto);

        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.That(okResult?.Value?.ToString(), Does.Contain("Product created successfully"));
    }

    [Test]
    public async Task DeleteProducts_ProductExists_ReturnsOk()
    {
        _mockProductService.Setup(x => x.DeleteAsync(1)).ReturnsAsync(true);

        var result = await _controller.DeleteProducts(1);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }


    [Test]
    public async Task DecrementStock_ValidInput_ReturnsOk()
    {
        _mockProductService.Setup(x => x.DecrementStockAsync(1, 5)).Returns(Task.CompletedTask);

        var result = await _controller.DecrementStock(1, 5);

        Assert.IsInstanceOf<OkObjectResult>(result);
    }
}
