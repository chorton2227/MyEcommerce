namespace MyEcommerce.Services.ProductService.API
{
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using MyEcommerce.Services.ProductService.Application;
    using MyEcommerce.Services.ProductService.Data;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

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
            AddAutoMapper(services);
            AddDataLayer(services);
            AddDbContexts(services);
            AddMvc(services);
            AddSwagger(services);
            AddMediatR(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyEcommerce.Services.ProductService.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DataSeeder.Seed(app);
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.Load("MyEcommerce.Services.ProductService.Application"));
        }

        private void AddDataLayer(IServiceCollection services)
        {
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductApplication, ProductApplication>();
        }

        private void AddDbContexts(IServiceCollection services)
        {
            if (Env.IsProduction())
            {
                services.AddDbContext<ProductContext>(opts =>
                {
                    opts.UseNpgsql("***REMOVED***");
                });
            }
            else
            {
                services.AddDbContext<ProductContext>(opts =>
                {
                    opts.UseInMemoryDatabase("InMem");
                });
            }
        }

        private void AddMvc(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins(
                        "https://product-service.ecommerce.chorton.dev",
                        "https://ecommerce.chorton.dev",
                        "https://www.ecommerce.com",
                        "https://localhost:5001",
                        "http://localhost:3000",
                        "https://localhost:3000"
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyEcommerce.Services.ProductService.API", Version = "v1" });
            });
        }

        private void AddMediatR(IServiceCollection services)
        {
            // services.AddMediatR(typeof(MyEcommerce.Core.Application.CommandHandlers.ClientRequestCommandHandler<,>));
            // services.AddMediatR(typeof(MyEcommerce.Services.ProductService.Application.CommandHandlers.ProductCreateCommandHandler));
            services.AddMediatR(AppDomain.CurrentDomain.Load("MyEcommerce.Core"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("MyEcommerce.Services.ProductService.Application"));
        }
    }
}
