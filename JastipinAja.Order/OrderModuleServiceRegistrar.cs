using JastipinAja.BuildingBlocks.Generic;
using JastipinAja.Order.Endpoints;
using JastipinAja.Order.Features.AcceptOrder;
using JastipinAja.Order.Features.CancelOrder;
using JastipinAja.Order.Features.CompleteOrder;
using JastipinAja.Order.Features.CreateOrder;
using JastipinAja.Order.Features.GetOrderById;
using JastipinAja.Order.Features.GetOrders;
using JastipinAja.Order.Features.MarkAsPaid;
using JastipinAja.Order.Features.MarkAsPurchased;
using JastipinAja.Order.Features.RefundOrder;
using JastipinAja.Order.Features.ShipOrder;
using JastipinAja.Order.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using FluentValidation;
using System.Text;
using JastipinAja.Order.Features.MarkAsReadyForHandover;

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
            services.AddScoped<AcceptOrderHandler>();
            services.AddScoped<MarkAsPaidHandler>();
            services.AddScoped<MarkAsPurchasedHandler>();
            services.AddScoped<ShipOrderHandler>();
            services.AddScoped<CompleteOrderHandler>();
            services.AddScoped<CancelOrderHandler>();
            services.AddScoped<RefundOrderHandler>();
            services.AddScoped<GetOrderByIdHandler>();
            services.AddScoped<GetOrdersHandler>();
            services.AddScoped<MarkAsReadyForHandoverHandler>();
            services.AddValidatorsFromAssembly(typeof(OrderModuleServiceRegistrar).Assembly, includeInternalTypes: true);

            return services;
        }

        public static IEndpointRouteBuilder MapOrderModule(this IEndpointRouteBuilder app)
        {
            app.MapOrderEndpoints();
            return app;
        }
    }
}
