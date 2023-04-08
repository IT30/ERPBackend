using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Entities
{
    [Table("cart_item", Schema = "Farm")]
    public class CartItemEntity
    {
        [Key]
        public Guid IDCartItem { get; set; }
        public Guid IDUser { get; set; }
        public Guid IDProduct { get; set; }
        public decimal CartAmount { get; set; } = 0!;
        public decimal CartPrice { get; set; } = 0!;
    }
}
