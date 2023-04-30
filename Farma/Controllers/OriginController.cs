using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/origin")]
    [Produces("application/json", "application/xml")]
    public class OriginController : ControllerBase
    {
        private readonly IOriginRepository originRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OriginController(IOriginRepository originRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.originRepository = originRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OriginDTO>> GetOrigins()
        {
            try
            {
                List<OriginEntity> origins = originRepository.GetOrigins();
                if (origins == null || origins.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<OriginDTO>>(origins));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{originID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<OriginDTO> GetOriginByID(Guid originID)
        {
            try
            {
                OriginEntity? origin = originRepository.GetOriginByID(originID);
                if (origin == null)
                    return NotFound();
                OriginDTO countryDTO = mapper.Map<OriginDTO>(origin);
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
        public ActionResult<OriginCreateDTO> CreateOrigin([FromBody] OriginCreateDTO originCreateDTO, [FromHeader] string authorization)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    OriginDTO origin = originRepository.CreateOrigin(originCreateDTO);
                    originRepository.SaveChanges();

                    string? location = linkGenerator.GetPathByAction("GetOrigin", "Origin", new { OriginID = origin.IDOrigin });

                    if (location != null)
                        return Created(location, origin);
                    else
                        return Created(string.Empty, origin);
                }
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OriginDTO> UpdateOrigin(OriginUpdateDTO originUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    OriginEntity? oldOrigin = originRepository.GetOriginByID(originUpdateDTO.IDOrigin);
                    if (oldOrigin == null)
                        return NotFound();

                    OriginEntity kolekcija = mapper.Map<OriginEntity>(originUpdateDTO);
                    mapper.Map(kolekcija, oldOrigin);
                    originRepository.SaveChanges();
                    return Ok(mapper.Map<OriginDTO>(kolekcija));
                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{originID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrigin(string originID)
        {
            try
            {
                if (!Guid.TryParse(originID, out _))
                    return BadRequest("The value '" + originID + "' is not valid.");

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    OriginEntity? origin = originRepository.GetOriginByID(Guid.Parse(originID));
                    if (origin == null)
                        return NotFound("Origin '" + originID + "' not found.");
                    originRepository.DeleteOrigin(Guid.Parse(originID));
                    originRepository.SaveChanges();

                    return NoContent();
                }
                return StatusCode(StatusCodes.Status403Forbidden, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetOriginsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
