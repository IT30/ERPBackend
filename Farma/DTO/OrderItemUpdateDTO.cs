using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class OrderItemUpdateDTO
    {
        [Required(ErrorMessage = "Order item must have an ID.")]
        public Guid IDOrderItem { get; set; }

        [Required(ErrorMessage = "Order item must have an order.")]
        public Guid IDOrder { get; set; }

        [Required(ErrorMessage = "Order item must have a product.")]
        public Guid IDProduct { get; set; }

        [Required(ErrorMessage = "Order must have an amount.")]
        public decimal OrderAmount { get; set; } = 0!;

        [Required(ErrorMessage = "Order must have a price.")]
        public decimal OrderPrice { get; set; } = 0!;
    }
}
