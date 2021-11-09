namespace MyEcommerce.Services.IdentityService.API.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IdentityServer4;
    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Mappers;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Configuration;

    public class ConfigurationDbContextSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            // await SeedClientsAsync(context, Config.GetClients(configuration));
            // await SeedIdentityResources(context, Config.GetIdentityResources());
            // await SeedApiResources(context, Config.GetApiResources());
        }

        public static async Task SeedClientsAsync(
            ConfigurationDbContext context,
            IEnumerable<Client> clients
        ) {
            if (context.Clients.Any())
            {
                return;
            }
            
            foreach (var client in clients)
            {
                context.Clients.Add(client.ToEntity());
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedIdentityResources(
            ConfigurationDbContext context,
            IEnumerable<IdentityResource> identityResources
        ) {
            if (context.IdentityResources.Any())
            {
                return;
            }

            foreach (var identityResource in identityResources)
            {
                context.IdentityResources.Add(identityResource.ToEntity());
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedApiResources(
            ConfigurationDbContext context,
            IEnumerable<ApiResource> apiResources
        ) {
            if (context.ApiResources.Any())
            {
                return;
            }

            foreach (var apiResource in apiResources)
            {
                context.ApiResources.Add(apiResource.ToEntity());
            }

            await context.SaveChangesAsync();
        }
    }
}