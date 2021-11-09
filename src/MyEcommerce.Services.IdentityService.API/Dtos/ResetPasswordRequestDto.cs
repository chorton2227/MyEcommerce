namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        
        [Required]
        public string Token { get; set; }

        [Required]
        public string Password { get; init; }

        [Required]
        public string ConfirmPassword { get; init; }
    }
}