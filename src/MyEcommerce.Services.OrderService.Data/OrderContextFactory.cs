namespace MyEcommerce.Services.OrderService.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using MyEcommerce.Core.Data;

    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public const string CONNECTION_STRING = "Host=localhost;Database=myecommerce;Username=postgres;Password=password";

        public OrderContext CreateDbContext(string[] args)
        {
            return new OrderContext(
                new DbContextOptionsBuilder<OrderContext>()
                    .UseNpgsql(CONNECTION_STRING)
                    .Options,
                new NoMediator()
            );
        }
    }
}