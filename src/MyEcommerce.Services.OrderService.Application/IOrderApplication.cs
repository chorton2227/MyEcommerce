namespace MyEcommerce.Services.OrderService.Application
{
    using System.Threading.Tasks;
    using MyEcommerce.Core.Application;
    using MyEcommerce.Services.OrderService.Application.Commands;
    using MyEcommerce.Services.OrderService.Application.Dtos;

    public interface IOrderApplication : IApplication
    {
        Task<OrderDto> CreateOrderAsync(OrderCreateCommand command);

        OrderDto GetOrder(string orderId, string userId);

        PaginatedOrdersDto GetOrders(OrderOptionsDto optionsDto);
    }
}