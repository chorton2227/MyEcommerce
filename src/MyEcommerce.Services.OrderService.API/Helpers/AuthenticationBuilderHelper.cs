namespace MyEcommerce.Services.OrderService.API.Helpers
{
    using System;
    using System.Text;
    using System.Text.Json;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class AuthenticationBuilderHelper
    {
        public static AuthenticationBuilder AddJwtBearerConfiguration(
            this AuthenticationBuilder builder,
            string issuer,
            string audience,
            string secret
        ) {
            return builder.AddJwtBearer(opts => {
                opts.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = !string.IsNullOrWhiteSpace(audience),
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
                opts.Events = new JwtBearerEvents {
                    OnChallenge = ctx => {
                        ctx.HandleResponse();
                        ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        ctx.Response.ContentType = "application/json";

                        // Default error
                        if (string.IsNullOrWhiteSpace(ctx.Error))
                        {
                            ctx.Error = "invalid_token";
                        }

                        // Default error description
                        if (string.IsNullOrWhiteSpace(ctx.ErrorDescription))
                        {
                            ctx.ErrorDescription = "A valid JWT is required.";
                        }

                        // Handle expired tokens
                        if (ctx.AuthenticateFailure != null && ctx.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = ctx.AuthenticateFailure as SecurityTokenExpiredException;
                            var expiration = authenticationException.Expires.ToString("o");
                            ctx.Response.Headers.Add("x-token-expired", expiration);
                            ctx.ErrorDescription = $"The token expired on {expiration}.";
                        }

                        // Write response
                        return ctx.Response.WriteAsync(JsonSerializer.Serialize(new {
                            error = ctx.Error,
                            error_description = ctx.ErrorDescription
                        }));
                    }
                };
            });
        }
    }
}
