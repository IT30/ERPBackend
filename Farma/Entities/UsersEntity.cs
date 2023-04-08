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
        public string Username { get; set; } = string.Empty!;
        public string FirstName { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty!;
        public string Adress { get; set; } = string.Empty!;
        public string City { get; set; } = string.Empty!;
        public string Phone { get; set; } = string.Empty!;
        public string UserRole { get; set; } = string.Empty!;

        [MaxLength(350, ErrorMessage = "User password hash can't have more than 350 characters.")]
        public string PwdHash { get; set; } = string.Empty!;

        [MaxLength(15, ErrorMessage = "User password salt can't have more than 15 characters.")]
        public string PwdSalt { get; set; } = string.Empty!;
    }
}
