namespace MyEcommerce.Services.OrderService.Application.Dtos
{
    public record OrderItemDto
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public decimal Total { get; set; }
    }
}