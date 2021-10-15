namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using MyEcommerce.Core.Domain;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag : Entity<TagId>
    {
        private readonly List<ProductTag> _productTags;

        [Required]
        public string Group { get; protected set; }

        [Required]
        public string Name { get; protected set; }
        
        public IReadOnlyCollection<ProductTag> ProductTags => _productTags;

        public Tag()
        {
            _productTags = new List<ProductTag>();
        }

        public Tag(string group, string name) : this()
        {
            Id = new TagId();
            Group = group;
            Name = name;
        }
    }
}