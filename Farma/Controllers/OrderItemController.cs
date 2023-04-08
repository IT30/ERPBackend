using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    }
}
