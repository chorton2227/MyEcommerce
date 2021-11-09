namespace MyEcommerce.Services.IdentityService.API.Services
{
    using System.Threading.Tasks;
    using MyEcommerce.Services.IdentityService.API.Models;

    public interface IEmailService
    {
        Task<bool> SendForgotPasswordAsync(User user, string token);
    }
}