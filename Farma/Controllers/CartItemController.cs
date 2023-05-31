using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Produces("application/json", "application/xml")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository cartItemRepository;
        private readonly IProductRepository productRepository;
        private readonly IUsersRepository usersRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public CartItemController(ICartItemRepository cartItemRepository, IProductRepository productRepository, IUsersRepository usersRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.cartItemRepository = cartItemRepository;
            this.productRepository = productRepository;
            this.usersRepository = usersRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CartItemDTO>> GetCartItems()
        {
            try
            {
                List<CartItemEntity> cartItems = cartItemRepository.GetCartItems();
                if (cartItems == null || cartItems.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<CartItemDTO>>(cartItems));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{cartItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<CartItemDTO> GetCartItemByID(Guid cartItemID)
        {
            try
            {
                CartItemEntity? cartItem = cartItemRepository.GetCartItemByID(cartItemID);
                if (cartItem == null)
                    return NotFound();
                CartItemDTO countryDTO = mapper.Map<CartItemDTO>(cartItem);
                return Ok(countryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("user/{userID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<CartItemDTO> GetCartItemByUser(Guid userID)
        {
            try
            {
                List<CartItemEntity> cartItems = cartItemRepository.GetCartItemsByUser(userID);
                if (cartItems == null || cartItems.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<CartItemDTO>>(cartItems));
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
        public ActionResult<CartItemCreateDTO> CreateCartItem([FromBody] CartItemCreateDTO cartItemCreateDTO, [FromHeader] string authorization)
        {
            try
            {
                string user = cartItemCreateDTO.IDUser.ToString();
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == user)
                {
                    Guid IDUser = cartItemCreateDTO.IDUser;
                    Guid IDProduct = cartItemCreateDTO.IDProduct;
                    UsersEntity? usersEntity = usersRepository.GetUserByID(IDUser);
                    ProductEntity? productEntity = productRepository.GetProductByID(IDProduct);
                    if (usersEntity != null && productEntity != null)
                    {
                        CartItemDTO cartItem = cartItemRepository.CreateCartItem(cartItemCreateDTO);
                        cartItemRepository.SaveChanges();

                        string? location = linkGenerator.GetPathByAction("GetCartItem", "CartItem", new { CartItemID = cartItem.IDCartItem });

                        if (location != null)
                            return Created(location, cartItem);
                        else
                            return Created(string.Empty, cartItem);
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "User or product do not exist");
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
        public ActionResult<CartItemDTO> UpdateCartItem(CartItemUpdateDTO cartItemUpdateDTO)
        {
            try
            {
                string user = cartItemUpdateDTO.IDUser.ToString();
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN" ||
                    HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value == user)
                {
                    Guid IDUser = cartItemUpdateDTO.IDUser;
                    Guid IDProduct = cartItemUpdateDTO.IDProduct;
                    UsersEntity? usersEntity = usersRepository.GetUserByID(IDUser);
                    ProductEntity? productEntity = productRepository.GetProductByID(IDProduct);
                    if (usersEntity != null && productEntity != null)
                    {
                        CartItemEntity? oldCartItem = cartItemRepository.GetCartItemByID(cartItemUpdateDTO.IDCartItem);
                        if (oldCartItem == null)
                            return NotFound();

                        CartItemEntity kolekcija = mapper.Map<CartItemEntity>(cartItemUpdateDTO);
                        mapper.Map(kolekcija, oldCartItem);
                        cartItemRepository.SaveChanges();
                        return Ok(mapper.Map<CartItemDTO>(kolekcija));
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "User or product do not exist");

                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{cartItemID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCartItem(string cartItemID)
        {
            try
            {
                if (!Guid.TryParse(cartItemID, out _))
                    return BadRequest("The value '" + cartItemID + "' is not valid.");


                CartItemEntity? cartItem = cartItemRepository.GetCartItemByID(Guid.Parse(cartItemID));
                if (cartItem == null)
                    return NotFound("CartItem '" + cartItemID + "' not found.");
                cartItemRepository.DeleteCartItem(Guid.Parse(cartItemID));
                cartItemRepository.SaveChanges();

                return NoContent();

            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }
    }
}
