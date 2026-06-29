using JastipinAja.BuildingBlocks.Generic;
using JastipinAja.Order.Endpoints;
using JastipinAja.Order.Features.CreateOrder;
using JastipinAja.Order.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order
{
    public static class OrderModuleServiceRegistrar
    {
        public static IServiceCollection AddOrderModule(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<OrderDbContext>(opt =>
            opt.UseNpgsql(config.GetConnectionString("OrderDb"),
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "ordering")));

            services.AddScoped<RunningNumberGenerator<OrderDbContext>>();
            services.AddScoped<CreateOrderHandler>();

            return services;
        }

        public static IEndpointRouteBuilder MapOrderModule(this IEndpointRouteBuilder app)
        {
            app.MapOrderEndpoints();
            return app;
        }
    }
}
