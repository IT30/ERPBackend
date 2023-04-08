namespace Farma.DTO
{
    public class UserDTO
    {
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
        public string PwdSalt { get; set; } = string.Empty!;
        public string PwdHash { get; set; } = string.Empty!;
    }
}
