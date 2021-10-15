namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate
{
    using System;
    using MyEcommerce.Core.Domain;

    public class CategoryId : Identifier
    {
        public CategoryId()
        {
        }

        public CategoryId(Guid value) : base(value)
        {
        }

        public CategoryId(string value) : base(value)
        {
        }
    }
}