namespace MyEcommerce.Services.OrderService.API.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyEcommerce.Services.OrderService.API.Helpers;
    using MyEcommerce.Services.OrderService.Application;
    using MyEcommerce.Services.OrderService.Application.Commands;
    using MyEcommerce.Services.OrderService.Application.Dtos;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IOrderApplication _orderApplication;

        public OrdersController(IMediator mediator, IOrderApplication orderApplication)
        {
            _mediator = mediator;
            _orderApplication = orderApplication;
        }

        [HttpGet(Name=nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<PaginatedOrdersDto> GetAll(
            [FromQuery] OrderOptionsDto options
        ) {
            options.UserId = User.GetId();
            return Ok(_orderApplication.GetOrders(options));
        }

        [HttpGet("{id}", Name=nameof(GetById))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<OrderDto> GetById(string id)
        {
            var order = _orderApplication.GetOrder(id, User.GetId());
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost(Name=nameof(CreateAsync))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OrderDto>> CreateAsync(
            [FromBody] OrderCreateDto orderCreateDto
        ) {
           var command = new OrderCreateCommand(orderCreateDto, User.GetId());
           var order = await _orderApplication.CreateOrderAsync(command);
           return CreatedAtRoute(nameof(GetById), new { Id = order.Id }, order);
        }
    }
}
