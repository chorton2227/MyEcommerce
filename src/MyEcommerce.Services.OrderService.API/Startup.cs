namespace MyEcommerce.Services.OrderService.API
{
    using System;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using MyEcommerce.Services.OrderService.API.Helpers;
    using MyEcommerce.Services.OrderService.Application;
    using MyEcommerce.Services.OrderService.Application.Services;
    using MyEcommerce.Services.OrderService.Data;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Env { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.Load("MyEcommerce.Services.OrderService.Application"));
            
            // MediatR
            services.AddMediatR(AppDomain.CurrentDomain.Load("MyEcommerce.Core"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("MyEcommerce.Services.OrderService.Application"));

            // Data and Application Layers
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderApplication, OrderApplication>();
            services.AddScoped<IEmailService, SendInBlueEmailService>();
            services.AddSingleton<SendInBlueEmailServiceConfig>(
                new SendInBlueEmailServiceConfig {
                    ApiKey = Configuration["SendInBlue:ApiKey"],
                    OrderReceiptTemplateId = long.Parse(Configuration["SendInBlue:OrderReceiptTemplateId"])
                });

            // DbContext
            services.AddDbContext<OrderContext>(opts => {
                opts.UseInMemoryDatabase("InMem");
            });

            // MVC
            services.AddControllers();
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins(
                        "https://myecommerce.chorton.dev",
                        "http://localhost:3000"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );
            });

            // JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearerConfiguration(
                    Configuration["JwtConfig:Issuer"],
                    Configuration["JwtConfig:Audience"],
                    Configuration["JwtConfig:Secret"]
                );

            // Swagger
            services.AddSwaggerGen(opts => {
                opts.SwaggerDoc(
                    "v1",
                    new OpenApiInfo {
                        Title = "MyEcommerce.Services.OrderService.API",
                        Version = "v1"
                    }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyEcommerce.Services.OrderService.API v1"));
            }

            app.UseCors("CorsPolicy");

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
