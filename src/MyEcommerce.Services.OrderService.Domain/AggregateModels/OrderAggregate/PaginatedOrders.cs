namespace MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate
{
    using System.Collections.Generic;

    public class PaginatedOrders
    {
        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public int TotalOrders { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}