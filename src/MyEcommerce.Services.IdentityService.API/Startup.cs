namespace MyEcommerce.Services.IdentityService.API
{
    using System;
    using System.Reflection;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using MyEcommerce.Services.IdentityService.API.Configurations;
    using MyEcommerce.Services.IdentityService.API.Data;
    using MyEcommerce.Services.IdentityService.API.Extensions;
    using MyEcommerce.Services.IdentityService.API.Models;
    using MyEcommerce.Services.IdentityService.API.Services;
    using SendGrid.Extensions.DependencyInjection;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.ConfigureNonBreakingSameSiteCookies();
            AddIdentityServices(services);
            AddAutoMapper(services);
            AddDataLayer(services);
            AddDbContexts(services);
            AddMvc(services);
            AddSwagger(services);
            AddMediatR(services);
            AddSendGrid(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy(new CookiePolicyOptions {
                MinimumSameSitePolicy = SameSiteMode.Lax,
                Secure = CookieSecurePolicy.Always
            });

            app.UseCors("CorsPolicy");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyEcommerce.Services.IdentityService.API v1"));
            }

            app.UseIdentityServer();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // DataSeeder.Seed(app);
        }

        private void AddIdentityServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MyIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            
            services
                .AddIdentityServer(options => {
                    options.IssuerUri = "null";
                    options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(Configuration))
                .AddAspNetIdentity<User>()
                .AddConfigurationStore(options => {
                    options.ConfigureDbContext = (builder) => {
                        builder.UseNpgsql(connectionString, options => {
                            options.MigrationsAssembly(migrationsAssembly);
                            options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), null);
                        });
                    };
                })
                .AddOperationalStore(options => {
                    options.ConfigureDbContext = (builder) => {
                        builder.UseNpgsql(connectionString, options => {
                            options.MigrationsAssembly(migrationsAssembly);
                            options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), null);
                        });
                    };
                });
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            // services.AddAutoMapper(AppDomain.CurrentDomain.Load("MyEcommerce.Services.IdentityService.Application"));
        }

        private void AddDataLayer(IServiceCollection services)
        {
            // services.AddScoped<ICatalogRepository, CatalogRepository>();
            // services.AddScoped<IProductRepository, ProductRepository>();
            // services.AddScoped<IProductApplication, ProductApplication>();
        }

        private void AddDbContexts(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<MyIdentityDbContext>(builder => {
                builder.UseNpgsql(connectionString, options => {
                    options.MigrationsAssembly(migrationsAssembly);
                    options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), null);
                });
            });
        }

        private void AddMvc(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(
                        "https://myecommerce.chorton.dev",
                        "http://localhost:3000"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyEcommerce.Services.IdentityService.API", Version = "v1" });
            });
        }

        private void AddMediatR(IServiceCollection services)
        {
        }

        private void AddSendGrid(IServiceCollection services)
        {
            services.AddTransient<IEmailService, SendInBlueEmailService>();
        }
    }
}
