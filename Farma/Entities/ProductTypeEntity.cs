using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Entities
{
    [Table("product_type", Schema = "Farm")]
    public class ProductTypeEntity
    {
        [Key]
        public Guid IDProductType { get; set; }
        public string Category { get; set; } = string.Empty!;
        public string CategoryDescription { get; set; } = string.Empty!;
    }
}
