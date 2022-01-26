namespace MyEcommerce.Services.OrderService.Application.Services
{
    public record SendInBlueEmailServiceConfig
    {
        public string ApiKey { get; set; }
        public long OrderReceiptTemplateId { get; set; }
    }
}