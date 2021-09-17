namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using MyEcommerce.Core.Domain;
    using System.Collections.Generic;

    public class ProductBrand : ValueObject
    {
        public string Name { get; set; }

        protected ProductBrand()
        {
        }

        public ProductBrand(string name)
        {
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}