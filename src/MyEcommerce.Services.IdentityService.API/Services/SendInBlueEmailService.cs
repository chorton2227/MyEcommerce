namespace MyEcommerce.Services.IdentityService.API.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MyEcommerce.Services.IdentityService.API.Models;
    using sib_api_v3_sdk.Api;
    using sib_api_v3_sdk.Client;
    using sib_api_v3_sdk.Model;
    
    public class SendInBlueEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendInBlueEmailService(IConfiguration configuration)
        {
            _configuration = configuration;

            Configuration.Default.AddApiKey("api-key", configuration["SendInBlue:ApiKey"]);
        }
        
        public async Task<bool> SendForgotPasswordAsync(User user, string token)
        {
            var api = new TransactionalEmailsApi();

            var clientUrl = _configuration["WebApp:ClientUrl"];
            var resetPasswordUrl = $"{clientUrl}/account/reset-password?token={token}";

            var email = new SendSmtpEmail
            {
                Subject = "Reset Password",
                HtmlContent = $"<a href=\"{resetPasswordUrl}\">Reset Password</a>",
                TextContent = $"Reset Password: {resetPasswordUrl}",
                Sender = new SendSmtpEmailSender("chorton dev", "noreply@chorton.dev"),
                To = new List<SendSmtpEmailTo>
                {
                    new SendSmtpEmailTo(user.Email, user.UserName)
                },
            };

            try
            {
                var response = await api.SendTransacEmailAsync(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}