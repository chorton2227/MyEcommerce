namespace MyEcommerce.Services.ProductService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags", ProductContext.DEFAULT_SCHEMA);

            builder.Ignore(c => c.DomainEvents);

            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasConversion(c => c.Value, id => new TagId(id))
                .IsRequired();

            builder
                .Property(c => c.Group)
                .IsRequired();

            builder
                .Property(c => c.Name)
                .IsRequired();
        }
    }
}