namespace ProductWarehouse.Service.DTO
{
    public class ProductUpdateDto
    {
        public string ItemName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
