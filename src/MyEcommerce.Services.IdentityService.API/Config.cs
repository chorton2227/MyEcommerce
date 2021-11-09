namespace MyEcommerce.Services.IdentityService.API
{
    using System.Collections.Generic;
    using IdentityServer4;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Configuration;

    public class Config
    {
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            var webAppClientUrl = configuration.GetValue<string>("WebApp:ClientUrl");
            var webAppClientSecret = configuration.GetValue<string>("WebApp:ClientSecret");
            return new List<Client>
            {
                new Client {
                    ClientId = "webapp",
                    ClientName = "WebApp",
                    ClientSecrets = { new Secret(webAppClientSecret.Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = { webAppClientUrl },
                    PostLogoutRedirectUris = { webAppClientUrl },
                    AllowedCorsOrigins = { webAppClientUrl },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "products",
                        "carts",
                        "orders",
                        "payments"
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("products", "Product Service"),
                new ApiResource("carts", "Cart Service"),
                new ApiResource("orders", "Order Service"),
                new ApiResource("payments", "Payment Service")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}