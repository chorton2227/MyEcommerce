namespace MyEcommerce.Services.IdentityService.API.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyEcommerce.Services.IdentityService.API.Models;

    public class MyIdentityDbContext : IdentityDbContext<User>
    {
        public MyIdentityDbContext(DbContextOptions<MyIdentityDbContext> options)
            : base(options)
        {
        }
    }
}