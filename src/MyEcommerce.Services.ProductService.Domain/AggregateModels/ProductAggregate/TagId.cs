namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using System;
    using MyEcommerce.Core.Domain;

    public class TagId : Identifier
    {
        public TagId()
        {
        }

        public TagId(Guid value) : base(value)
        {
        }

        public TagId(string value) : base(value)
        {
        }
    }
}