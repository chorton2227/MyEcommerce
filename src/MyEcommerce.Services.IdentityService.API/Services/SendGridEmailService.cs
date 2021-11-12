namespace MyEcommerce.Services.IdentityService.API.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using MyEcommerce.Services.IdentityService.API.Models;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class SendGridEmailService : IEmailService
    {
        private readonly ISendGridClient _client;
        private readonly IConfiguration _configuration;

        public SendGridEmailService(ISendGridClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<bool> SendForgotPasswordAsync(User user, string token)
        {
            var clientUrl = _configuration["WebApp:ClientUrl"];
            var resetPasswordUrl = $"{clientUrl}/account/reset-password?token={token}";

            var msg = new SendGridMessage
            {
                From = new EmailAddress("noreply@chorton.dev", "chorton dev"),
                Subject = "Reset Password",
                HtmlContent = $"<a href=\"{resetPasswordUrl}\">Reset Password</a>",
                PlainTextContent = $"Reset Password: {resetPasswordUrl}"
            };
            
            msg.AddTo(new EmailAddress(user.Email, user.UserName));

            var response = await _client.SendEmailAsync(msg).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}