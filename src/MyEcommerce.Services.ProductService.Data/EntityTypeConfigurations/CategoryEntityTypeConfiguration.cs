namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;

    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", ProductContext.DEFAULT_SCHEMA);

            builder.Ignore(c => c.DomainEvents);

            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasConversion(c => c.Value, id => new CategoryId(id))
                .IsRequired();

            builder
                .Property(c => c.Name)
                .IsRequired();
            
            builder
                .HasOne(c => c.Catalog)
                .WithMany(cat => cat.Categories)
                .IsRequired();
        }
    }
}