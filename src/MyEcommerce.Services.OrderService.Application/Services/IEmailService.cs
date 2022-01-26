namespace MyEcommerce.Services.OrderService.Application.Services
{
    using System.Threading.Tasks;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public interface IEmailService
    {
        Task<bool> SendOrderReceipt(Order order);
    }
}