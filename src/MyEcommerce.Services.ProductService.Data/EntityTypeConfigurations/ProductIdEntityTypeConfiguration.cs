namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Core.Domain.Common;

    public class ProductIdEntityTypeConfiguration : IEntityTypeConfiguration<ProductId>
    {
        public void Configure(EntityTypeBuilder<ProductId> builder)
        {
            builder.ToTable("ProductIds", ProductContext.DEFAULT_SCHEMA);

            builder.HasKey(p => p.Value);
        }
    }
}