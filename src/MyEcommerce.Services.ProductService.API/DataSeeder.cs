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

    public static class DataSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ProductContext>();

            // Data already seeded?
            if (context.Products.Any())
            {
                return;
            }

            // Set the randomizer seed to generate repeatable data sets.
            Randomizer.Seed = new Random(8675309);

            var fakeProduct = new Faker<Product>()
                .RuleFor(p => p.Id, f => new ProductId())
                .RuleFor(p => p.ProductType, f => new ProductType(f.Commerce.Department()))
                .RuleFor(p => p.ProductBrand, f => new ProductBrand(f.Company.CompanyName()))
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(p => p.ImageUri, f => f.Image.LoremPixelUrl(LoremPixelCategory.Random))
                .RuleFor(p => p.AvailableStock, f => (uint)0)
                .RuleFor(p => p.RestockTreshold, f => (uint)0)
                .RuleFor(p => p.MaxStockThreshold, f => (uint)0)
                .RuleFor(p => p.OnReorder, f => false);
            
            for (var i = 0; i < 100; i++) {
                context.Products.Add(fakeProduct.Generate());
            }

            context.SaveChanges();
        }
    }
}