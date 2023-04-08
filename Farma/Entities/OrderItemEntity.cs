using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Entities
{
    [Table("order_item", Schema = "Farm")]
    public class OrderItemEntity
    {
        [Key]
        public Guid IDOrderItem { get; set; }
        public Guid IDOrder { get; set; }
        public Guid IDProduct { get; set; }
        public decimal OrderAmount { get; set; } = 0!;
        public decimal OrderPrice { get; set; } = 0!;
    }
}
