using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json", "application/xml")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository usersRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public UsersController(IUsersRepository usersRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UserDTO>> GetUsers()
        {
            try
            {
                List<UsersEntity> users = usersRepository.GetUsers();
                if (users == null || users.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<UserDTO>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
