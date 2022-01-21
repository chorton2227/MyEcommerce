namespace MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate
{
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;

    public interface IOrderRepository : IRepository<Order>
    {
        Order Create(Order order);

        PaginatedOrders GetAll(OrderOptions options);

        Order GetOne(OrderId orderId, string userId);
    }
}