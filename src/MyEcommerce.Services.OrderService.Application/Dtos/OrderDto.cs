namespace MyEcommerce.Services.OrderService.Application.Dtos
{
    using System;
    using System.Collections.Generic;

    public record OrderDto
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public OrderStatusDto Status { get; set; }

        public AddressDto BillingAddress { get; set; } 

        public AddressDto DeliveryAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string ChargeId { get; set; }

        public decimal Total { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}