using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

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

        [HttpGet("{OriginID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<OriginDTO> GetOriginByID(Guid OriginID)
        {
            try
            {
                OriginEntity? origin = originRepository.GetOriginByID(OriginID);
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
    }
}
