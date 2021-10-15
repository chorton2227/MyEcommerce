namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;
    using MyEcommerce.Services.ProductService.Domain.DomainEvents;

    public class Product : Entity<ProductId>, IAggregateRoot
    {
        private readonly List<ProductCategory> _productCategories;

        private readonly List<ProductTag> _productTags;

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

        public decimal? SalePrice { get; protected set; }

        public string ImageFileName { get; protected set; }

        public string ImageUri { get; protected set; }

        public uint AvailableStock { get; protected set; }

        public uint RestockTreshold { get; protected set; }

        public uint MaxStockThreshold { get; protected set; }

        public bool OnReorder { get; protected set; }

        public bool IsNew { get; protected set; }

        public bool OnSale {
            get {
                return SalePrice.HasValue;
            }
        }

        public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories;

        public IReadOnlyCollection<ProductTag> ProductTags => _productTags;

        public Product()
        {
            _productCategories = new List<ProductCategory>();
            _productTags = new List<ProductTag>();
        }

        public Product(
            ProductType productType,
            ProductBrand productBrand,
            string name,
            string description,
            decimal price,
            decimal? salePrice,
            string imageFileName,
            string imageUri,
            uint availableStock,
            uint restockTreshold,
            uint maxStockThreshold,
            bool onReorder,
            bool isNew) : this()
        {
            Id = new ProductId();
            ProductType = productType;
            ProductBrand = productBrand;
            Name = name;
            Description = description;
            Price = price;
            SalePrice = salePrice;
            ImageFileName = imageFileName;
            ImageUri = imageUri;
            AvailableStock = availableStock;
            RestockTreshold = restockTreshold;
            MaxStockThreshold = maxStockThreshold;
            OnReorder = onReorder;
            IsNew = isNew;

            AddDomainEvent(new ProductCreatedDomainEvent(this));
        }

        public void AddCategory(Category newCategopry)
        {
            var existingCategory = _productCategories.Find(pc => pc.Category == newCategopry);
            if (existingCategory != null)
            {
                // Category already associated with product
                return;
            }

            _productCategories.Add(new ProductCategory {
                Category = newCategopry,
                CategoryId = newCategopry.Id,
                Product = this,
                ProductId = this.Id
            });
        }

        public void AddTag(Tag newTag)
        {
            var existingTag = _productTags.FirstOrDefault(pt => pt.Tag == newTag);
            if (existingTag != null)
            {
                // Tag already associated with product
                return;
            }

            _productTags.Add(new ProductTag {
                Tag = newTag,
                TagId = newTag.Id,
                Product = this,
                ProductId = this.Id
            });
        }
    }
}