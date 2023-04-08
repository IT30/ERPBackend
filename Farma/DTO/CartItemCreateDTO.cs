using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class CartItemCreateDTO
    {
        [Required(ErrorMessage = "Cart item must have a user.")]
        public Guid IDUser { get; set; }

        [Required(ErrorMessage = "Cart item must have a product.")]
        public Guid IDProduct { get; set; }

        [Required(ErrorMessage = "Cart item must have an amount.")]
        public decimal CartAmount { get; set; } = 0!;

        [Required(ErrorMessage = "Cart item must have a price.")]
        public decimal CartPrice { get; set; } = 0!;
    }
}
