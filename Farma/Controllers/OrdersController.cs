using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using Farma.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Farma.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Produces("application/json", "application/xml")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IUsersRepository usersRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public OrdersController(IOrdersRepository ordersRepository, IUsersRepository usersRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.usersRepository = usersRepository;
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

        [HttpGet("{ordersID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<OrdersDTO> GetOrdersByID(Guid ordersID)
        {
            try
            {
                OrdersEntity? orders = ordersRepository.GetOrderByID(ordersID);
                if (orders == null)
                    return NotFound();
                OrdersDTO countryDTO = mapper.Map<OrdersDTO>(orders);
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
        public ActionResult<OrdersCreateDTO> CreateOrders([FromBody] OrdersCreateDTO ordersCreateDTO, [FromHeader] string authorization)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    Guid IDUser = ordersCreateDTO.IDUser;
                    UsersEntity? usersEntity = usersRepository.GetUserByID(IDUser);
                    if (usersEntity != null)
                    {
                        OrdersDTO orders = ordersRepository.CreateOrder(ordersCreateDTO);
                        ordersRepository.SaveChanges();

                        string? location = linkGenerator.GetPathByAction("GetOrders", "Orders", new { OrdersID = orders.IDOrder });

                        if (location != null)
                            return Created(location, orders);
                        else
                            return Created(string.Empty, orders);
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "User do not exist");
                }
                else
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, "Access forbidden.");
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrdersDTO> UpdateOrders(OrdersUpdateDTO ordersUpdateDTO)
        {
            try
            {
                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    Guid IDUser = ordersUpdateDTO.IDUser;
                    UsersEntity? usersEntity = usersRepository.GetUserByID(IDUser);
                    if (usersEntity != null )
                    {
                        OrdersEntity? oldOrder = ordersRepository.GetOrderByID(ordersUpdateDTO.IDOrder);
                        if (oldOrder == null)
                            return NotFound();

                        OrdersEntity kolekcija = mapper.Map<OrdersEntity>(ordersUpdateDTO);
                        mapper.Map(kolekcija, oldOrder);
                        ordersRepository.SaveChanges();
                        return Ok(mapper.Map<OrdersDTO>(kolekcija));
                    }
                    else
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "User do not exist");

                }
                else
                    return StatusCode(StatusCodes.Status403Forbidden, "Access forbiden");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{ordersID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteOrders(string ordersID)
        {
            try
            {
                if (!Guid.TryParse(ordersID, out _))
                    return BadRequest("The value '" + ordersID + "' is not valid.");

                if (HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value == "ADMIN")
                {
                    OrdersEntity? orders = ordersRepository.GetOrderByID(Guid.Parse(ordersID));
                    if (orders == null)
                        return NotFound("Orders '" + ordersID + "' not found.");
                    ordersRepository.DeleteOrder(Guid.Parse(ordersID));
                    ordersRepository.SaveChanges();

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
