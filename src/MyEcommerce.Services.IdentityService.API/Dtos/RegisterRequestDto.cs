namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public record RegisterRequestDto
    {
        [Required]
        public string Username { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        [Required]
        public string ConfirmPassword { get; init; }
    }
}