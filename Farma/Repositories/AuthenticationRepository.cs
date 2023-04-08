using Farma.DTO;
using Farma.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Farma.Repositories
{
	public class AuthenticationRepository : IAuthenticationRepository
	{
		private readonly FarmaContext context;
		private readonly IConfiguration configuration;

		public AuthenticationRepository(FarmaContext context, IConfiguration configuration)
		{
			this.context = context;
			this.configuration = configuration;
		}


		public bool AuthenticateUser(UserAuthenticationDTO userAuthenticationDTO)
		{
			if (userAuthenticationDTO.Username != null)
			{
				UsersEntity? user = context.Users.FirstOrDefault(e => e.Username == userAuthenticationDTO.Username);
				if (user == null)
					return false;
				if (VerifyPassword(userAuthenticationDTO.Password, user.PwdHash, user.PwdSalt))
					return true;
			}
			return false;
		}

		public string GenerateJWT(string? userIdentity)
		{
			string? key = configuration["JWT:Key"];
			if (key != null)
			{
				Claim[]? claims = null;
				UsersEntity? user = null;
				if (userIdentity != null && Guid.TryParse(userIdentity, out _))
					user = context.Users.FirstOrDefault(e => e.IDUser == Guid.Parse(userIdentity));
				else if (userIdentity != null)
					user = context.Users.FirstOrDefault(e => e.Username == userIdentity);
				if (user != null)
				{
					claims = new[]
					{
						new Claim(ClaimTypes.NameIdentifier, user.IDUser.ToString()),
						new Claim(ClaimTypes.Name, user.Username),
						new Claim(ClaimTypes.Role, user.UserRole),
						new Claim(ClaimTypes.GivenName, user.FirstName),
						new Claim(ClaimTypes.Surname, user.LastName),
						new Claim(ClaimTypes.Email, user.Email)
					};
				}
			
				SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
				SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

				JwtSecurityToken token = new(configuration["JWT:Issuer"],
										configuration["JWT:Audience"],
										claims,
										expires: DateTime.Now.AddMinutes(120),
										signingCredentials: credentials);

				return new JwtSecurityTokenHandler().WriteToken(token);
			}
			return string.Empty;
		}


		private static bool VerifyPassword(string password, string pwdHash, string pwdSalt)
		{
			if (password != null && pwdHash != null && pwdSalt != null)
			{
				var saltBytes = Convert.FromBase64String(pwdSalt);
				var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000);
				if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == pwdHash)
					return true;
			}
			return false;
		}
	}
}
