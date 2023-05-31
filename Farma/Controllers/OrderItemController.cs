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
        private readonly IOrdersRepository ordersRepository;
        private readonly IProductRepository productRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OrderItemController(IOrderItemRepository orderItemRepository, IOrdersRepository ordersRepository, IProductRepository productRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.orderItemRepository = orderItemRepository;
            this.ordersRepository = ordersRepository;
            this.productRepository = productRepository;
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

        [HttpGet("order/{orderID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<OrderItemDTO> GetOrderItemByOrder(Guid orderID)
        {
            try
            {
                List<OrderItemEntity> orderItems = orderItemRepository.GetOrderItemsByOrder(orderID);
                if (orderItems == null || orderItems.Count == 0)
                    return NoContent();

                return Ok(mapper.Map<List<OrderItemDTO>>(orderItems));
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
        public ActionResult<OrderItemCreateDTO> CreateOrderItem([FromBody] OrderItemCreateDTO orderItemCreateDTO, [FromHeader] string authorization)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value != null)
                {
                    Guid IDOrder = orderItemCreateDTO.IDOrder;
                    Guid IDProduct = orderItemCreateDTO.IDProduct;
                    OrdersEntity? ordersEntity = ordersRepository.GetOrderByID(IDOrder);
                    ProductEntity? productEntity = productRepository.GetProductByID(IDProduct);
                    if (ordersEntity != null && productEntity != null)
                    {
                        OrderItemDTO orderItem = orderItemRepository.CreateOrderItem(orderItemCreateDTO);
                        orderItemRepository.SaveChanges();

                        string? location = linkGenerator.GetPathByAction("GetOrderItem", "OrderItem", new { OrderItemID = orderItem.IDOrderItem });

                        if (location != null)
                            return Created(location, orderItem);
                        else
                            return Created(string.Empty, orderItem);
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Order or product do not exist");
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
        public ActionResult<OrderItemDTO> UpdateOrderItem(OrderItemUpdateDTO orderItemUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    Guid IDOrder = orderItemUpdateDTO.IDOrder;
                    Guid IDProduct = orderItemUpdateDTO.IDProduct;
                    OrdersEntity? ordersEntity = ordersRepository.GetOrderByID(IDOrder);
                    ProductEntity? productEntity = productRepository.GetProductByID(IDProduct);
                    if (ordersEntity != null && productEntity != null)
                    {
                        OrderItemEntity? oldOrderItem = orderItemRepository.GetOrderItemByID(orderItemUpdateDTO.IDOrderItem);
                        if (oldOrderItem == null)
                            return NotFound();

                        OrderItemEntity kolekcija = mapper.Map<OrderItemEntity>(orderItemUpdateDTO);
                        mapper.Map(kolekcija, oldOrderItem);
                        orderItemRepository.SaveChanges();
                        return Ok(mapper.Map<OrderItemDTO>(kolekcija));
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Order or product do not exist");

                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");

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
