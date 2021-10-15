namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class TagIdEntityTypeConfiguration : IEntityTypeConfiguration<TagId>
    {
        public void Configure(EntityTypeBuilder<TagId> builder)
        {
            builder.ToTable("TagIds", ProductContext.DEFAULT_SCHEMA);

            builder.HasKey(p => p.Value);
        }
    }
}