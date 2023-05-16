using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
    public class OrdersUpdateDTO
    {
        [Required(ErrorMessage = "Order must have an ID.")]
        public Guid IDOrder { get; set; }

        [Required(ErrorMessage = "Order must have a user.")]
        public Guid IDUser { get; set; }

        [Required(ErrorMessage = "Order must have a total price.")]
        public decimal TotalOrderPrice { get; set; } = 0!;

    }
}
