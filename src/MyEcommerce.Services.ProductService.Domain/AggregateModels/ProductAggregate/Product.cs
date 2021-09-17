namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using System.ComponentModel.DataAnnotations;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.ProductService.Domain.DomainEvents;

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        [Required]
        public ProductType ProductType { get; protected set; }

        [Required]
        public ProductBrand ProductBrand { get; protected set; }

        [Required]
        public string Name { get; protected set; }

        [Required]
        public string Description { get; protected set; }

        [Required]
        public decimal Price { get; protected set; }

        public string ImageFileName { get; protected set; }

        public string ImageUri { get; protected set; }

        public uint AvailableStock { get; protected set; }

        public uint RestockTreshold { get; protected set; }

        public uint MaxStockThreshold { get; protected set; }

        public bool OnReorder { get; protected set; }

        protected Product()
        {
        }

        public Product(
            ProductType productType,
            ProductBrand productBrand,
            string name,
            string description,
            decimal price,
            string imageFileName,
            string imageUri,
            uint availableStock,
            uint restockTreshold,
            uint maxStockThreshold,
            bool onReorder)
        {
            Id = new ProductId();
            ProductType = productType;
            ProductBrand = productBrand;
            Name = name;
            Description = description;
            Price = price;
            ImageFileName = imageFileName;
            ImageUri = imageUri;
            AvailableStock = availableStock;
            RestockTreshold = restockTreshold;
            MaxStockThreshold = maxStockThreshold;
            OnReorder = onReorder;

            AddDomainEvent(new ProductCreatedDomainEvent(this));
        }
    }
}