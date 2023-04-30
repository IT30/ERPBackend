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
        private readonly IOriginRepository originRepository;
        private readonly IClassRepository classRepository;
        private readonly IProductTypeRepository productTypeRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IOriginRepository originRepository, IClassRepository classRepository, IProductTypeRepository productTypeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.originRepository = originRepository;
            this.classRepository = classRepository;
            this.productTypeRepository = productTypeRepository;
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

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductCreateDTO> CreateProduct([FromBody] ProductCreateDTO productCreateDTO, [FromHeader] string authorization)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    Guid IDOrigin = productCreateDTO.IDOrigin;
                    Guid IDProductType = productCreateDTO.IDProductType;
                    Guid IDClass = productCreateDTO.IDClass;
                    ClassEntity? classEntity = classRepository.GetClassByID(IDClass);
                    ProductTypeEntity? productTypeEntity = productTypeRepository.GetProductTypeByID(IDProductType);
                    OriginEntity? originEntity = originRepository.GetOriginByID(IDOrigin);
                    if (originEntity != null && productTypeEntity != null && classEntity != null)
                    {
                        ProductDTO product = productRepository.CreateProduct(productCreateDTO);
                        productRepository.SaveChanges();

                        string? location = linkGenerator.GetPathByAction("GetProduct", "Product", new { ProductID = product.IDProduct });

                        if (location != null)
                            return Created(location, product);
                        else
                            return Created(string.Empty, product);
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Product Type, class or origin do not exist");
                }
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.InnerException);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDTO> UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    Guid IDOrigin = productUpdateDTO.IDOrigin;
                    Guid IDProductType = productUpdateDTO.IDProductType;
                    Guid IDClass = productUpdateDTO.IDClass;
                    ClassEntity? classEntity = classRepository.GetClassByID(IDClass);
                    ProductTypeEntity? productTypeEntity = productTypeRepository.GetProductTypeByID(IDProductType);
                    OriginEntity? originEntity = originRepository.GetOriginByID(IDOrigin);
                    if (originEntity != null && productTypeEntity != null && classEntity != null)
                    {
                        ProductEntity? oldProduct = productRepository.GetProductByID(productUpdateDTO.IDProduct);
                        if (oldProduct == null)
                            return NotFound();

                        ProductEntity kolekcija = mapper.Map<ProductEntity>(productUpdateDTO);
                        mapper.Map(kolekcija, oldProduct);
                        productRepository.SaveChanges();
                        return Ok(mapper.Map<ProductDTO>(kolekcija));
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Product Type, class or origin do not exist");

                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");

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
