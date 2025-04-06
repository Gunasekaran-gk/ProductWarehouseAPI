using System.ComponentModel.DataAnnotations;

namespace ProductWarehouse.Enitity.Entities
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public string BrandName { get; set; }
        public decimal Price { get; set; }
        public int TotalQuantity { get; set; }
        public bool Active { get; set; }
    }
}
