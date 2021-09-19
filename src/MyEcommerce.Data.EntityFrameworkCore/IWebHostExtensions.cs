namespace MyEcommerce.Data.EntityFrameworkCore
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Polly;

    public static class IWebHostExtensions
    {
        private static readonly int RetryCount = 5;
        
        public static IWebHost MigrateDbContext<TDbContext>(this IWebHost host, Action<TDbContext, IServiceProvider> seeder = null)
            where TDbContext : BaseDbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>();
                var context = services.GetService<TDbContext>();

                try
                {
                    var retryPolicy = Policy
                        .Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: RetryCount,
                            sleepDurationProvider: attempt => TimeSpan.FromSeconds(attempt),
                            onRetry: (ex, timeout, attempt, context) =>
                            {
                                logger.LogWarning(ex, "[{ContextName}] Attempt {Attempt} of {RetryCount} failed to migrate DbContext ({ExceptionMessage}).", nameof(TDbContext), attempt, RetryCount, ex.Message);
                            });

                    retryPolicy.Execute(() =>
                    {
                        context.Database.Migrate();
                        seeder?.Invoke(context, services);
                    });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "[{ContextName}] Failed to migrate DbContext ({ExceptionMessage}).", nameof(TDbContext), ex.Message);
                    throw;
                }
            }

            return host;
        }
    }
}