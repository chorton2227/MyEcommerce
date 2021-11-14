namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    public record MeResponseDto
    {
        public bool LoggedIn { get; set; }

        public string Jwt { get; set; }
    }
}