using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Entities
{
    [Table("origin", Schema = "Farm")]
    public class OriginEntity
    {
        [Key]
        public Guid IDOrigin { get; set; }
        public string OriginName { get; set; } = string.Empty!;
        public string OriginDescription { get; set; } = string.Empty!;
    }
}
