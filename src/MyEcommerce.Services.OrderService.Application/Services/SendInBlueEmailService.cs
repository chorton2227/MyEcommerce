namespace MyEcommerce.Services.OrderService.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;
    using sib_api_v3_sdk.Api;
    using sib_api_v3_sdk.Client;
    using sib_api_v3_sdk.Model;

    public class SendInBlueEmailService : IEmailService
    {
        private readonly SendInBlueEmailServiceConfig _config;

        public SendInBlueEmailService(SendInBlueEmailServiceConfig config)
        {
            _config = config;

            Configuration.Default.AddApiKey("api-key", _config.ApiKey);
        }

        public async Task<bool> SendOrderReceipt(Order order)
        {
            var api = new TransactionalEmailsApi();
            var email = new SendSmtpEmail
            {
                TemplateId = _config.OrderReceiptTemplateId,
                To = new List<SendSmtpEmailTo> {
                    new SendSmtpEmailTo(order.Email)
                },
                Params = new {
                    id = order.Id.Value,
                    date = order.OrderDate,
                    total = ConvertDecimalToString(order.Total),
                    deliveryAddress = new {
                        firstName = order.DeliveryAddress.FirstName,
                        lastName = order.DeliveryAddress.LastName,
                        street1 = order.DeliveryAddress.Street1,
                        street2 = order.DeliveryAddress.Street2,
                        city = order.DeliveryAddress.City,
                        state = order.DeliveryAddress.State,
                        country = order.DeliveryAddress.Country,
                        zipCode = order.DeliveryAddress.ZipCode,
                    },
                    items = order.OrderItems.Select(item => new {
                        name = item.Name,
                        imageUrl = item.ImageUrl,
                        price = ConvertDecimalToString(item.Price),
                        quantity = item.Quantity,
                        total = ConvertDecimalToString(item.Price * item.Quantity)
                    })
                }
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

        private string ConvertDecimalToString(decimal value)
        {
            return value.ToString("N2");
        }
    }
}