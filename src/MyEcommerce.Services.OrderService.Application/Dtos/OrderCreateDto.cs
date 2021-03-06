namespace MyEcommerce.Services.OrderService.Application.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public record OrderCreateDto
    {
        [Required]
        public string ChargeId { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public AddressDto DeliveryAddress { get; set; }

        [Required]
        public List<OrderItemDto> OrderItems { get; set; }
    }
}