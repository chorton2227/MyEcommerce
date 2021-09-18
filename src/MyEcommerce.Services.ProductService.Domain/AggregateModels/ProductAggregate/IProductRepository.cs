namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using System.Collections.Generic;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;

    public interface IProductRepository : IRepository<Product>
    {
        void Create(Product product);

        IEnumerable<Product> GetAll();

        Product GetById(ProductId productId);
    }
}