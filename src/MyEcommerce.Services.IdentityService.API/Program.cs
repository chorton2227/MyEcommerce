using System;
using System.IO;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyEcommerce.Data.EntityFrameworkCore;
using MyEcommerce.Services.IdentityService.API;
using MyEcommerce.Services.IdentityService.API.Data;
using Serilog;

var configuration = GetConfiguration();
Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("[{AppName}] Building web host...", MyEcommerce.Services.IdentityService.API.Program.AppName);
    var host = BuildWebHost(configuration, args);

    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var env = services.GetRequiredService<IWebHostEnvironment>();
        if (env.IsProduction())
        {
            Log.Information("[{AppName}] Migrating database...", MyEcommerce.Services.IdentityService.API.Program.AppName);
            host.MigrateDbContext<PersistedGrantDbContext>()
                .MigrateDbContext<MyIdentityDbContext>()
                .MigrateDbContext<ConfigurationDbContext>();
        }
    }

    Log.Information("[{AppName}] Starting web host...", MyEcommerce.Services.IdentityService.API.Program.AppName);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "[{AppName}] Unexpected exception ({ExceptionMessage}).", MyEcommerce.Services.IdentityService.API.Program.AppName, ex.Message);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
    return builder.Build();
}

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    var seqServerUrl = configuration.GetValue("Serilog:SeqServerUrl", "http://seq");
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", Program.AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(seqServerUrl)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

IWebHost BuildWebHost(IConfiguration configuration, string[] args)
{
    return WebHost
        .CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog()
        .UseKestrel()
        .Build();
}

namespace MyEcommerce.Services.IdentityService.API
{
    public static class Program
    {
        #region Public Properties

        public static string AppName => typeof(Program).Namespace;

        #endregion Public Properties
    }
}
