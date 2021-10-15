namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;

    public class ProductCategory
    {
        public CategoryId CategoryId { get; set; }

        public Category Category { get; set; }

        public ProductId ProductId { get; set; }

        public Product Product { get; set; }
    }
}