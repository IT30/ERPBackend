using System.ComponentModel.DataAnnotations;

namespace Farma.DTO
{
	public class UserAuthenticationDTO
	{
		[Required(ErrorMessage = "User has to have a username.")]
		[MaxLength(15, ErrorMessage = "Username can't have more than 15 characters.")]
		public string Username { get; set; } = string.Empty!;

		[Required(ErrorMessage = "User has to have a password hash.")]
		[MinLength(5, ErrorMessage = "User password can't have less than 5 characters.")]
		[MaxLength(25, ErrorMessage = "User password can't have more than 25 characters.")]
		public string Password { get; set; } = string.Empty!;
	}
}
