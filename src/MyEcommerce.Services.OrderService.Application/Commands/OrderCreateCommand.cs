namespace MyEcommerce.Services.OrderService.Application.Commands
{
    using MyEcommerce.Core.Application.Commands;
    using MyEcommerce.Services.OrderService.Application.Dtos;

    public class OrderCreateCommand : ICommand<OrderDto>
    {
        public OrderCreateDto OrderCreateDto { get; private set; }

        public string UserId { get; private set; }

        public OrderCreateCommand(OrderCreateDto orderCreateDto, string userId)
        {
            OrderCreateDto = orderCreateDto;
            UserId = userId;
        }
    }
}