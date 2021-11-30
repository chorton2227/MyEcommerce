namespace MyEcommerce.Services.OrderService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", OrderContext.DEFAULT_SCHEMA);

            builder.Ignore(oi => oi.DomainEvents);

            builder.HasKey(oi => oi.Id);

            builder
                .Property(oi => oi.Id)
                .HasConversion(oi => oi.Value, id => new OrderItemId(id))
                .IsRequired();
            
            builder
                .Property(oi => oi.ProductId)
                .HasConversion(oi => oi.Value, id => new ProductId(id))
                .IsRequired();
            
            builder
                .Property(oi => oi.Name)
                .IsRequired();
            
            builder
                .Property(oi => oi.Price)
                .IsRequired();
            
            builder
                .Property(oi => oi.Quantity)
                .IsRequired();
            
            builder
                .Property(oi => oi.ImageUrl)
                .IsRequired();
            
            builder
                .Property(oi => oi.Total)
                .IsRequired();
        }
    }
}