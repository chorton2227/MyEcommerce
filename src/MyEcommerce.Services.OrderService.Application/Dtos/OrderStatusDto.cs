namespace MyEcommerce.Services.OrderService.Application.Dtos
{
    public record OrderStatusDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}