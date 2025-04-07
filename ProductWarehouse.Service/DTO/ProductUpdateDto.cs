using System.ComponentModel.DataAnnotations;

namespace ProductWarehouse.Service.DTO
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Item name is required.")]
        [MinLength(3, ErrorMessage = "Item name cannot be empty.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Brand name is required.")]
        [MinLength(3, ErrorMessage = "Brand name cannot be empty.")]
        public string BrandName { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price must be zero or positive.")]
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
