namespace MyEcommerce.Services.OrderService.Application.Dtos
{
    public record OrderOptionsDto
    {
        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public string UserId { get; set; }
    }
}