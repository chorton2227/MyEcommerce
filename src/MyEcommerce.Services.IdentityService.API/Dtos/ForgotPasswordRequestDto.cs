namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public record ForgotPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
    }
}