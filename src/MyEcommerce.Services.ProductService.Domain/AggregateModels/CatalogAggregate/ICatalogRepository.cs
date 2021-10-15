namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate
{
    using System.Collections.Generic;
    using MyEcommerce.Core.Domain;

    public interface ICatalogRepository : IRepository<Catalog>
    {
        void Create(Catalog catalog);

        IEnumerable<Catalog> GetAll();

        Catalog GetById(CatalogId catalogId);

        IEnumerable<Category> GetCategoriesByCatalogId(CatalogId catalogId);
    }
}