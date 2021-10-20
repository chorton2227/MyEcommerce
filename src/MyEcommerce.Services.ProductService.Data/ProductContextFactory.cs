namespace MyEcommerce.Services.ProductService.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using MyEcommerce.Core.Data;

    public class ProductContextFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public const string CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=MyEcommerce;Integrated Security=true";

        public ProductContext CreateDbContext(string[] args)
        {
            return new ProductContext(
                new DbContextOptionsBuilder<ProductContext>()
                    .UseNpgsql(CONNECTION_STRING)
                    .Options,
                new NoMediator()
            );
        }
    }
}