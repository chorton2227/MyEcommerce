namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate
{
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category : Entity<CategoryId>
    {
        private readonly List<ProductCategory> _productCategories;

        [Required]
        public string Name { get; set; }

        public Catalog Catalog { get; set; }

        public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories;

        public Category()
        {
            _productCategories = new List<ProductCategory>();
        }

        public Category(Catalog catalog, string name)
        {
            Id = new CategoryId();
            Catalog = catalog;
            Name = name;
        }
    }
}