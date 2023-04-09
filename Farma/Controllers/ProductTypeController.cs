using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("{productTypeID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ProductTypeDTO> GetProductTypeByID(Guid productTypeID)
        {
            try
            {
                ProductTypeEntity? productType = productTypeRepository.GetProductTypeByID(productTypeID);
                if (productType == null)
                    return NotFound();
                ProductTypeDTO countryDTO = mapper.Map<ProductTypeDTO>(productType);
                return Ok(countryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{productTypeID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProductType(string productTypeID)
        {
            try
            {
                if (!Guid.TryParse(productTypeID, out _))
                    return BadRequest("The value '" + productTypeID + "' is not valid.");

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    ProductTypeEntity? productType = productTypeRepository.GetProductTypeByID(Guid.Parse(productTypeID));
                    if (productType == null)
                        return NotFound("ProductType '" + productTypeID + "' not found.");
                    productTypeRepository.DeleteProductType(Guid.Parse(productTypeID));
                    productTypeRepository.SaveChanges();

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
