namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using MyEcommerce.Core.Domain.Common;

    public class ProductTag
    {
        public TagId TagId { get; set; }

        public Tag Tag { get; set; }

        public ProductId ProductId { get; set; }

        public Product Product { get; set; }
    }
}