using Farma.DTO;
using Farma.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Farma.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api")]
	[Produces("application/json", "application/xml")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationRepository authenticationRepository;

		public AuthenticationController(IAuthenticationRepository authenticationRepository)
		{
			this.authenticationRepository = authenticationRepository;
		}

		/// <response code="200">Returns the user entity's token.</response>
		/// <response code="401">The user entity is not authorized.</response>
		/// <response code="500">A server error occurred while authenticating user entity.</response>
		[AllowAnonymous]
		[HttpPost("auth")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult Authenticate([FromBody] UserAuthenticationDTO userAuthenticationDTO)
		{
			try
			{
				if (authenticationRepository.AuthenticateUser(userAuthenticationDTO))
					return Ok(new { token = authenticationRepository.GenerateJWT(userAuthenticationDTO.Username) });
				return Unauthorized("User not authorized.");
			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
			}
		}

		/// <response code="200">Returns the renewed token.</response>
		/// <response code="500">A server error occurred while renewing user entity's token.</response>
		[HttpGet("renewAuth")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult RenewAuth()
		{
			try
			{
				return Ok(new { token = authenticationRepository.GenerateJWT(HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value) });
			}
			catch (Exception exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
			}
		}
	}
}
