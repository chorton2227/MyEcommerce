namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", ProductContext.DEFAULT_SCHEMA);

            builder.Ignore(p => p.DomainEvents);

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasConversion(p => p.Value, id => new ProductId(id))
                .IsRequired();

            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .IsRequired();
                

            builder.OwnsOne(p => p.ProductType, productType =>
            {
                productType
                    .Property(pt => pt.Name)
                    .IsRequired();
            });
                

            builder.OwnsOne(p => p.ProductBrand, productBrand =>
            {
                productBrand
                    .Property(pt => pt.Name)
                    .IsRequired();
            });
        }
    }
}