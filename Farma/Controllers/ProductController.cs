using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/product")]
    [Produces("application/json", "application/xml")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ProductDTO>> GetProducts()
        {
            try
            {
                List<ProductEntity> products = productRepository.GetProducts();
                if (products == null || products.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<ProductDTO>>(products));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
