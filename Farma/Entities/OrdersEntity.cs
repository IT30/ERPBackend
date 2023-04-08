using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Entities
{
    [Table("orders", Schema = "Farm")]
    public class OrdersEntity
    {
        [Key]
        public Guid IDOrder { get; set; }
        public Guid IDUser { get; set; }
        public decimal TotalOrderPrice { get; set; } = 0!;
        public DateTime? TransactionDate { get; set; }
    }
}
