namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MyEcommerce.Core.Domain;

    public class Catalog : Entity<CatalogId>, IAggregateRoot
    {
        private readonly List<Category> _categories;

        [Required]
        public string Name { get; set; }

        public IReadOnlyCollection<Category> Categories => _categories;

        public Catalog()
        {
            _categories = new List<Category>();
        }

        public Catalog(string name, CatalogId id = null)
        {
            Id = id ?? new CatalogId();
            Name = name;
        }
    }
}