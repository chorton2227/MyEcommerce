namespace MyEcommerce.Services.ProductService.Data
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Data.EntityFrameworkCore;
    using MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductContext : BaseDbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "ProductService";

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }
    }
}