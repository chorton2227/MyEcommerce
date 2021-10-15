namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;

    public class CategoryIdEntityTypeConfiguration : IEntityTypeConfiguration<CategoryId>
    {
        public void Configure(EntityTypeBuilder<CategoryId> builder)
        {
            builder.ToTable("CategoryIds", ProductContext.DEFAULT_SCHEMA);

            builder.HasKey(p => p.Value);
        }
    }
}