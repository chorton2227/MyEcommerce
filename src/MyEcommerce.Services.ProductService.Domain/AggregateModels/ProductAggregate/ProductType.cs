namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using MyEcommerce.Core.Domain;
    using System.Collections.Generic;

    public class ProductType : ValueObject
    {
        public string Name { get; set; }

        protected ProductType()
        {
        }

        public ProductType(string name)
        {
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}