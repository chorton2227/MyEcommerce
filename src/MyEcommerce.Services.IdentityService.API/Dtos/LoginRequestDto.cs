namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public record LoginRequestDto
    {
        [Required]
        public string Username { get; init; }

        [Required]
        public string Password { get; init; }
    }
}