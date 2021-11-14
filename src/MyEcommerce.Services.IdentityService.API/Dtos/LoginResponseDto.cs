namespace MyEcommerce.Services.IdentityService.API.Dtos
{
    using System.Collections.Generic;
    
    public record LoginResponseDto
    {
        public bool Success { get; set; }

        public string Jwt { get; set; }

        public IEnumerable<FieldErrorDto> FieldErrors { get; init; }
    }
}