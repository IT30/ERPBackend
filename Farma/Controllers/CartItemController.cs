using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
