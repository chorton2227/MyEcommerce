namespace MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate
{
    using System.ComponentModel.DataAnnotations;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;

    public class OrderItem : Entity<OrderItemId>
    {
        [Required]
        public ProductId ProductId { get; private set; }

        [Required]
        public string Name { get; private set; }

        [Required]
        public decimal Price { get; private set; }

        [Required]
        public int Quantity { get; private set; }

        [Required]
        public string ImageUrl { get; private set; }

        public decimal Total
        {
            get { return Price * Quantity; } 
            set
            {
                // required for ef 
            }
        }

        public OrderItem()
        {
        }

        public OrderItem(
            ProductId productId,
            string name,
            decimal price,
            int quantity,
            string imageUrl
        ) {
            Id = new OrderItemId();
            ProductId = productId;
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageUrl = imageUrl;
        }
    }
}