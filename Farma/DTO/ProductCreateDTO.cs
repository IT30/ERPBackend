using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product must have a type.")]
        public Guid IDProductType { get; set; }

        [Required(ErrorMessage = "Product must have a class.")]
        public Guid IDClass { get; set; }

        [Required(ErrorMessage = "Product must have a origin.")]
        public Guid IDOrigin { get; set; }

        [Required(ErrorMessage = "Product must have a name.")]
        [MaxLength(25, ErrorMessage = "Product name must be less than 25 characters.")]
        public string ProductName { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Product must have an amount.")]
        public decimal SupplyKG { get; set; } = 0!;

        [Required(ErrorMessage = "Product must have a price.")]
        public decimal PriceKG { get; set; } = 0!;

        [Required(ErrorMessage = "Product must have a picture.")]
        [MaxLength(100, ErrorMessage = "Picture url must be less than 100 characters.")]
        public string ProductPictureURL { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Product must have a description.")]
        [MaxLength(400, ErrorMessage = "Product description must be less than 400 characters.")]
        public string ProductDescription { get; set; } = string.Empty!;

        [Required(ErrorMessage = "Product must have a discount percentage.")]
        public decimal DiscountPercentage { get; set; } = 0!;
    }
}
