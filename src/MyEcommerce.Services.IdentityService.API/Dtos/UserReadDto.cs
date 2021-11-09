namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    public record UserReadDto
    {
        public string Id { get; init; }

        public string Username { get; init; }
    }
}