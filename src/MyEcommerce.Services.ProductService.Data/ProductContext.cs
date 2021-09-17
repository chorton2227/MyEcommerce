namespace MyEcommerce.Services.ProductService.Data
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Data.EntityFrameworkCore;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductContext : BaseDbContext, IUnitOfWork
    {
        public ProductContext(DbContextOptions<ProductContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }
    }
}