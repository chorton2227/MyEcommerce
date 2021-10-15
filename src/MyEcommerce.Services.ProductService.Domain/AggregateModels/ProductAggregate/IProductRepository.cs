namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;

    public interface IProductRepository : IRepository<Product>
    {
        void Create(Product product);

        PaginatedProducts GetAll(ProductOptions options);

        Product GetById(ProductId productId);
    }
}