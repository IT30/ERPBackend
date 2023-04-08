using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Entities
{
    [Table("class", Schema = "Farm")]
    public class ClassEntity
    {
        [Key]
        public Guid IDClass { get; set; }
        public string Class { get; set; } = string.Empty!;
        public string ClassDescription { get; set; } = string.Empty!;
    }
}
