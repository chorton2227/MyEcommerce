namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate
{
    using System;
    using MyEcommerce.Core.Domain;

    public class CatalogId : Identifier
    {
        public CatalogId()
        {
        }

        public CatalogId(Guid value) : base(value)
        {
        }

        public CatalogId(string value) : base(value)
        {
        }
    }
}