namespace MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate
{
    using System;
    using MyEcommerce.Core.Domain;

    public class OrderItemId : Identifier
    {
        public OrderItemId()
        {
        }

        public OrderItemId(Guid value) : base(value)
        {
        }

        public OrderItemId(string value) : base(value)
        {
        }
    }
}