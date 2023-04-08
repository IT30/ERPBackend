using Farma.DTO;

namespace Farma.Repositories
{
	public interface IAuthenticationRepository
	{

		public bool AuthenticateUser(UserAuthenticationDTO userAuthenticationDTO);


		public string GenerateJWT(string? userIdentity);
	}
}
