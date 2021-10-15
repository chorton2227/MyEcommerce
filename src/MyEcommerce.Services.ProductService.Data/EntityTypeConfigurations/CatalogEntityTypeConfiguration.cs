namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;

    public class CatalogEntityTypeConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.ToTable("Catalogs", ProductContext.DEFAULT_SCHEMA);

            builder.Ignore(c => c.DomainEvents);

            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasConversion(c => c.Value, id => new CatalogId(id))
                .IsRequired();

            builder
                .Property(c => c.Name)
                .IsRequired();
            
            builder
                .HasMany(c => c.Categories)
                .WithOne(cat => cat.Catalog)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}