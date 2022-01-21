namespace MyEcommerce.Services.OrderService.Data.EntityTypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", OrderContext.DEFAULT_SCHEMA);

            builder.Ignore(o => o.DomainEvents);

            builder.HasKey(o => o.Id);

            builder
                .Property(o => o.Id)
                .HasConversion(o => o.Value, id => new OrderId(id))
                .IsRequired();
            
            builder
                .Property(o => o.UserId)
                .IsRequired();
            
            builder
                .Property(o => o.OrderDate)
                .IsRequired();
            
            builder
                .Property(o => o.ChargeId)
                .IsRequired();
            
            builder
                .Property(o => o.Email)
                .IsRequired();
            
            builder
                .Property(o => o.Total)
                .IsRequired();
            
            builder
                .Property(o => o.Status)
                .HasConversion(
                    status => status.Id,
                    statusId => OrderStatus.Parse<OrderStatus>(statusId)
                )
                .IsRequired();
            
            builder
                .OwnsOne(o => o.DeliveryAddress, a => {
                    a.Property(aa => aa.FirstName)
                        .IsRequired();

                    a.Property(aa => aa.LastName)
                        .IsRequired();

                    a.Property(aa => aa.Street1)
                        .IsRequired();
                        
                    a.Property(aa => aa.City)
                        .IsRequired();

                    a.Property(aa => aa.State)
                        .IsRequired();

                    a.Property(aa => aa.Country)
                        .IsRequired();

                    a.Property(aa => aa.ZipCode)
                        .IsRequired();
                });
            
            var orderItemNav = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            orderItemNav.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}