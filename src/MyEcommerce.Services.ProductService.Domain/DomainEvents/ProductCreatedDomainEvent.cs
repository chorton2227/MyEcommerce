namespace MyEcommerce.Services.ProductService.Domain.DomainEvents
{
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductCreatedDomainEvent : IDomainEvent
    {
        public Product Product { get; }

        public ProductCreatedDomainEvent(Product product)
        {
            Product = product;
        }
    }
}