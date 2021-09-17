namespace MyEcommerce.Services.ProductService.API
{
    using System;
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            AddAutoMapper(services);
            AddDataLayer(services);
            AddDbContexts(services);
            AddControllers(services);
            AddSwagger(services);
            return BuildContainer(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyEcommerce.Services.ProductService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private void AddDataLayer(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductApplication, ProductApplication>();
        }

        private void AddDbContexts(IServiceCollection services)
        {
            if (Env.IsProduction())
            {
                services.AddDbContext<ProductContext>(opts =>
                {
                    opts.UseSqlServer(Configuration.GetConnectionString("Default"));
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

        private void AddControllers(IServiceCollection services)
        {
            services.AddControllers();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyEcommerce.Services.ProductService.API", Version = "v1" });
            });
        }

        private AutofacServiceProvider BuildContainer(IServiceCollection services)
        {
            var container = new ContainerBuilder();
            container.Populate(services);
            
            // Mediator
            container
                .RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            container.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            return new AutofacServiceProvider(container.Build());
        }
    }
}
