namespace MyEcommerce.Services.OrderService.Data
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Data.EntityFrameworkCore;
    using MyEcommerce.Services.OrderService.Data.EntityTypeConfigurations;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderContext : BaseDbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "OrderService";

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
        }
    }
}