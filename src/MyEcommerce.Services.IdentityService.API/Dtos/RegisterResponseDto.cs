namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    using System.Collections.Generic;

    public record RegisterResponseDto
    {
        public IEnumerable<FieldErrorDto> FieldErrors { get; init; }

        public UserReadDto User { get; init; }
    }
}