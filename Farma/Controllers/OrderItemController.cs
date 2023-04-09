using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/orderItem")]
    [Produces("application/json", "application/xml")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OrderItemController(IOrderItemRepository orderItemRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.orderItemRepository = orderItemRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OrderItemDTO>> GetOrderItem()
        {
            try
            {
                List<OrderItemEntity> orderItem = orderItemRepository.GetOrderItems();
                if (orderItem == null || orderItem.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<OrderItemDTO>>(orderItem));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{orderItemID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<OrderItemDTO> GetOrderItemByID(Guid orderItemID)
        {
            try
            {
                OrderItemEntity? orderItem = orderItemRepository.GetOrderItemByID(orderItemID);
                if (orderItem == null)
                    return NotFound();
                OrderItemDTO countryDTO = mapper.Map<OrderItemDTO>(orderItem);
                return Ok(countryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{orderItemID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrderItem(string orderItemID)
        {
            try
            {
                if (!Guid.TryParse(orderItemID, out _))
                    return BadRequest("The value '" + orderItemID + "' is not valid.");

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    OrderItemEntity? orderItem = orderItemRepository.GetOrderItemByID(Guid.Parse(orderItemID));
                    if (orderItem == null)
                        return NotFound("OrderItem '" + orderItemID + "' not found.");
                    orderItemRepository.DeleteOrderItem(Guid.Parse(orderItemID));
                    orderItemRepository.SaveChanges();

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
