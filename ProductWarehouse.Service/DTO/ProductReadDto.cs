namespace ProductWarehouse.Service.DTO
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public int TotalQuantity { get; set; }
        public bool Active { get; set; }
    }
}
