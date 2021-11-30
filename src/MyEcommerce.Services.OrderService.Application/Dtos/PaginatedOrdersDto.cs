namespace MyEcommerce.Services.OrderService.Application.Dtos
{
    using System.Collections.Generic;
    
    public record PaginatedOrdersDto
    {
        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public int TotalOrders { get; set; }

        public bool HasMore { get; set; }

        public List<OrderDto> Orders { get; set; }
    }
}