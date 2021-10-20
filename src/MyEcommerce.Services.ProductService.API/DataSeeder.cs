namespace MyEcommerce.Services.ProductService.API
{
    using Bogus;
    using Bogus.DataSets;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using MyEcommerce.Services.ProductService.Data;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;
    using MyEcommerce.Core.Domain.Common;
    using System;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;

    public static class DataSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            // Set the randomizer seed to generate repeatable data sets.
            Randomizer.Seed = new Random(8675309);

            SeedCatalogs(applicationBuilder);
            SeedTags(applicationBuilder);
            SeedProducts(applicationBuilder);
        }

        public static void SeedCatalogs(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ProductContext>();

            // Data already seeded?
            if (context.Catalogs.Any())
            {
                return;
            }

            var catalogId = new CatalogId("CatalogId-3bfc1e05-6ce6-42ca-90f6-71556e1f1c7e");
            var catalog = new Catalog("default", catalogId);
            context.Catalogs.Add(catalog);
            context.SaveChanges();

            var categoryNames = new[] { "Men", "Women", "Boys", "Girls", "Unisex" };
            foreach (var categoryName in categoryNames)
            {
                var category = new Category(catalog, categoryName);
                context.Categories.Add(category);
            }
            context.SaveChanges();
        }

        public static void SeedTags(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ProductContext>();

            // Data already seeded?
            if (context.Tags.Any())
            {
                return;
            }

            var colorGroup = "Color";
            var colorTagNames = new[] {
                "Black", "Blue", "Green", "Grey", "Orange",
                "Pink", "Red", "White", "Yellow"
            };

            foreach (var colorTagName in colorTagNames)
            {
                var tag = new Tag(colorGroup, colorTagName);
                context.Tags.Add(tag);
            }

            context.SaveChanges();
        }

        public static void SeedProducts(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ProductContext>();

            // Data already seeded?
            if (context.Products.Any())
            {
                return;
            }

            var fakeProduct = new Faker<Product>()
                .RuleFor(p => p.Id, f => new ProductId())
                .RuleFor(p => p.ProductType, f => new ProductType(f.Commerce.Department()))
                .RuleFor(p => p.ProductBrand, f => new ProductBrand(f.Company.CompanyName()))
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(max: 45)))
                .RuleFor(p => p.SalePrice, (f, p) => f.Random.Number(1, 10) <= 3 ? decimal.Parse(f.Commerce.Price(max: p.Price - 1)) : null)
                .RuleFor(p => p.ImageUri, (f, p) => f.Image.PlaceholderUrl(640, 480, p.Name))
                .RuleFor(p => p.AvailableStock, f => (uint)0)
                .RuleFor(p => p.RestockTreshold, f => (uint)0)
                .RuleFor(p => p.MaxStockThreshold, f => (uint)0)
                .RuleFor(p => p.OnReorder, f => false)
                .RuleFor(p => p.IsNew, f => f.Random.Number(1, 10) <= 3);
            
            for (var i = 0; i < 100; i++) {
                var generatedProduct = fakeProduct.Generate();

                generatedProduct.AddCategory(
                    context.Categories
                        .OrderBy(r => Guid.NewGuid())
                        .FirstOrDefault()
                );

                generatedProduct.AddTag(
                    context.Tags
                        .OrderBy(r => Guid.NewGuid())
                        .FirstOrDefault()
                );

                context.Products.Add(generatedProduct);
            }

            context.SaveChanges();
        }
    }
}