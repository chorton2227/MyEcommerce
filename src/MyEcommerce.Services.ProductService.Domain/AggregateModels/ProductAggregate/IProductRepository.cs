namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using MyEcommerce.Core.Domain;
    
    public interface IProductRepository : IRepository<Product>
    {
        void Create(Product product);
    }
}