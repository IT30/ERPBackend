using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;

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
        private readonly IWebHostEnvironment _env;

        public UsersController(IUsersRepository usersRepository, LinkGenerator linkGenerator, IMapper mapper, IWebHostEnvironment env)
        {
            this.usersRepository = usersRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this._env = env;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<UserDTO>> GetUsers()
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    List<UsersEntity> users = usersRepository.GetUsers();
                    if (users == null || users.Count == 0)
                        return NoContent();
                    return Ok(mapper.Map<List<UserDTO>>(users));
                }
                return StatusCode(StatusCodes.Status403Forbidden, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpGet("{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDTO> GetUser(string userID)
        {
            try
            {
                if (!Guid.TryParse(userID, out _))
                    return BadRequest("The value '" + userID + "' is not valid.");
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == userID)
                {
                    UsersEntity? user = usersRepository.GetUserByID(Guid.Parse(userID));
                    if (user == null)
                        return NotFound("User '" + userID + "' not found.");
                    return Ok(mapper.Map<UserDTO>(user));
                }
                return StatusCode(StatusCodes.Status403Forbidden, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserCreateDTO> CreateUsers([FromBody] UserCreateDTO userCreateDTO)
        {
            try
            {
                List<UsersEntity> users = usersRepository.GetUsers();
                if (users.Find(e => e.Username == userCreateDTO.Username) == null &&
                    users.Find(e => e.Email == userCreateDTO.Email) == null)
                {
                    UserDTO userDTO = usersRepository.CreateUser(userCreateDTO);
                    usersRepository.SaveChanges();

                    string? location = linkGenerator.GetPathByAction("GetUsers", "Users", new { userID = userDTO.IDUser });

                    if (location != null)
                        return Created(location, userDTO);
                    else
                        return Created(string.Empty, userDTO);
                }
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Username or email already exists.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDTO> UpdateUser(UserUpdateDTO usersUpdateDTO)
        {
            try
            {
                if (usersRepository.GetUserByID(Guid.Parse(usersUpdateDTO.IDUser)) == null)
                    return NotFound();
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == usersUpdateDTO.IDUser)
                {
                    List<UsersEntity> users = usersRepository.GetUsers();
                    UsersEntity? tempUser = users.Find(e => e.IDUser == Guid.Parse(usersUpdateDTO.IDUser));
                    if (tempUser != null)
                        users.Remove(tempUser);
                    if (users.Find(e => e.Username == usersUpdateDTO.Username) == null &&
                        users.Find(e => e.Email == usersUpdateDTO.Email) == null)
                    {
                        UserDTO usersDTO = usersRepository.UpdateUser(usersUpdateDTO);
                        usersRepository.SaveChanges();

                        return Ok(usersDTO);
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Username or email already exists.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbidden.");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpDelete("{userID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(string userID)
        {
            try
            {
                if (!Guid.TryParse(userID, out _))
                    return BadRequest("The value '" + userID + "' is not valid.");

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == userID)
                {
                    UsersEntity? user = usersRepository.GetUserByID(Guid.Parse(userID));
                    if (user == null)
                        return NotFound("User '" + userID + "' not found.");
                    usersRepository.DeleteUser(Guid.Parse(userID));
                    usersRepository.SaveChanges();

                    return NoContent();
                }
                return StatusCode(StatusCodes.Status403Forbidden, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [Route("photo")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
