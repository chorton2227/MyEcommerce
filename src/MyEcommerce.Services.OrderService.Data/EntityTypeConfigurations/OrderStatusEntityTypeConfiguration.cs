namespace MyEcommerce.Services.OrderService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderStatusEntityTypeConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatuses", OrderContext.DEFAULT_SCHEMA);

            builder.HasKey(os => os.Id);

            builder.Property(os => os.Id)
                .HasDefaultValue(OrderStatus.Submitted.Id)
                .ValueGeneratedNever()
                .IsRequired();
            
            builder.Property(os => os.Name)
                .HasDefaultValue(OrderStatus.Submitted.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}