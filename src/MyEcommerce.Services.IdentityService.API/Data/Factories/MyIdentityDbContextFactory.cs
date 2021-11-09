namespace MyEcommerce.Services.IdentityService.API.Data.Factories
{
    using System.IO;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class MyIdentityDbContextFactory : IDesignTimeDbContextFactory<MyIdentityDbContext>
    {
        public MyIdentityDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<MyIdentityDbContext>();
            optionsBuilder.UseNpgsql(config["ConnectionString"]);

            return new MyIdentityDbContext(optionsBuilder.Options);
        }
    }
}