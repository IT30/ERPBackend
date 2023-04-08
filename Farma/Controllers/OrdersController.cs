using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Produces("application/json", "application/xml")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OrdersController(IOrdersRepository ordersRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<OrdersDTO>> GetOrders()
        {
            try
            {
                List<OrdersEntity> orders = ordersRepository.GetOrders();
                if (orders == null || orders.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<OrdersDTO>>(orders));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
