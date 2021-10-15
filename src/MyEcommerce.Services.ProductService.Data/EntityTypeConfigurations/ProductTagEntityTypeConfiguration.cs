namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductTagEntityTypeConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.ToTable("ProductTags", ProductContext.DEFAULT_SCHEMA);

            builder.HasKey(pc => new { pc.ProductId, pc.TagId });

            builder
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pc => pc.Tag)
                .WithMany(c => c.ProductTags)
                .HasForeignKey(pc => pc.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}