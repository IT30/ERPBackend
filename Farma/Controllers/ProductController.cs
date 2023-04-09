using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("{productID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ProductDTO> GetProductByID(Guid productID)
        {
            try
            {
                ProductEntity? product = productRepository.GetProductByID(productID);
                if (product == null)
                    return NotFound();
                ProductDTO countryDTO = mapper.Map<ProductDTO>(product);
                return Ok(countryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{productID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProduct(string productID)
        {
            try
            {
                if (!Guid.TryParse(productID, out _))
                    return BadRequest("The value '" + productID + "' is not valid.");

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ProductEntity? product = productRepository.GetProductByID(Guid.Parse(productID));
                    if (product == null)
                        return NotFound("Product '" + productID + "' not found.");
                    productRepository.DeleteProduct(Guid.Parse(productID));
                    productRepository.SaveChanges();

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
