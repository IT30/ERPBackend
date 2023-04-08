using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Entities
{
    [Table("users", Schema = "Farm")]
    public class UsersEntity
    {
        [Key]
        public Guid IDUser { get; set; }
        public string? ProfilePictureURL { get; set; }
        public string Email { get; set; } = string.Empty!;
        public string UserPassword { get; set; } = string.Empty!;
        public string FirstName { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty!;
        public string Adress { get; set; } = string.Empty!;
        public string City { get; set; } = string.Empty!;
        public string Phone { get; set; } = string.Empty!;
        public string UserRole { get; set; } = string.Empty!;
    }
}
