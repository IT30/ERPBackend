using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
