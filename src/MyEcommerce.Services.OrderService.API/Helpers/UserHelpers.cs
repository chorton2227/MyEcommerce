namespace MyEcommerce.Services.OrderService.API.Helpers
{
    using System.Security.Claims;

    public static class UserHelpers
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)
                ?? principal.FindFirst(claim => claim.Type == "sub");
            if (userIdClaim != null && !string.IsNullOrWhiteSpace(userIdClaim.Value))
            {
                return userIdClaim.Value;
            }

            return null;
        }
    }
}