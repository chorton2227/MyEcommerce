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
                .Property(o => o.Total)
                .IsRequired();
            
            builder
                .OwnsOne(o => o.BillingAddress, a => {
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
            
            builder
                .OwnsOne(o => o.DeliveryAddress, a => {
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
            
            builder
                .HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey("_orderStatusId");
            
            var orderItemNav = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            orderItemNav.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}