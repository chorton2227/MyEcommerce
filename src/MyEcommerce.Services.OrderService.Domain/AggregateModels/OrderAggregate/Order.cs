namespace MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;

    public class Order : Entity<OrderId>, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems;

        [Required]
        public string UserId { get; private set; }

        [Required]
        public OrderStatus Status { get; private set; }

        [Required]
        public Address DeliveryAddress { get; private set; }

        [Required]
        public DateTimeOffset OrderDate { get; private set; }

        [Required]
        public string ChargeId { get; private set; }

        [Required]
        public string Email { get; private set; }

        [Required]
        public decimal Total
        { 
            get
            {
                return _orderItems.Sum(item => item.Total);
            }
            set
            {
                // required for ef 
            }
        }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(
            string userId,
            Address deliveryAddress,
            string chargeId,
            string email
        ) : this() {
			Id = new OrderId();
            UserId = userId;
            DeliveryAddress = deliveryAddress;
            ChargeId = chargeId;
            Email = email;
            OrderDate = DateTimeOffset.UtcNow;
            Status = OrderStatus.Submitted;
        }

        public void AddOrderItem(OrderItem item)
        {
            _orderItems.Add(item);
        }
    }
}