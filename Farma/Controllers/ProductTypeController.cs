using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/productType")]
    [Produces("application/json", "application/xml")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeRepository productTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProductTypeController(IProductTypeRepository productTypeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.productTypeRepository = productTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ProductTypeDTO>> GetProductTypes()
        {
            try
            {
                List<ProductTypeEntity> productTypees = productTypeRepository.GetProductTypes();
                if (productTypees == null || productTypees.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<ProductTypeDTO>>(productTypees));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
