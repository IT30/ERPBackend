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
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public CartItemController(ICartItemRepository cartItemRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.cartItemRepository = cartItemRepository;
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

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    CartItemEntity? cartItem = cartItemRepository.GetCartItemByID(Guid.Parse(cartItemID));
                    if (cartItem == null)
                        return NotFound("CartItem '" + cartItemID + "' not found.");
                    cartItemRepository.DeleteCartItem(Guid.Parse(cartItemID));
                    cartItemRepository.SaveChanges();

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
