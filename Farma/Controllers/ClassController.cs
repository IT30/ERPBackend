using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/class")]
    [Produces("application/json", "application/xml")]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository classRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ClassController(IClassRepository classRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.classRepository = classRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ClassDTO>> GetClasses()
        {
            try
            {
                List<ClassEntity> classes = classRepository.GetClasses();
                if (classes == null || classes.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<ClassDTO>>(classes));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{classID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ClassDTO> GetClassByID(Guid classID)
        {
            try
            {
                ClassEntity? classe = classRepository.GetClassByID(classID);
                if (classe == null)
                    return NotFound();
                ClassDTO countryDTO = mapper.Map<ClassDTO>(classe);
                return Ok(countryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ClassCreateDTO> CreateClass([FromBody] ClassCreateDTO classCreateDTO, [FromHeader] string authorization)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ClassDTO classe = classRepository.CreateClass(classCreateDTO);
                    classRepository.SaveChanges();

                    string? location = linkGenerator.GetPathByAction("GetClass", "Class", new { ClassID = classe.IDClass });

                    if (location != null)
                        return Created(location, classe);
                    else
                        return Created(string.Empty, classe);
                }
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpDelete("{classID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteClass(string classID)
        {
            try
            {
                if (!Guid.TryParse(classID, out _))
                    return BadRequest("The value '" + classID + "' is not valid.");

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ClassEntity? classe = classRepository.GetClassByID(Guid.Parse(classID));
                    if (classe == null)
                        return NotFound("Class '" + classID + "' not found.");
                    classRepository.DeleteClass(Guid.Parse(classID));
                    classRepository.SaveChanges();

                    return NoContent();
                }
                return StatusCode(StatusCodes.Status403Forbidden, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
