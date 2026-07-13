using FluentValidation;
using JastipinAja.BuildingBlocks.Generic;
using JastipinAja.Payment.Endpoints;
using JastipinAja.Payment.Features.CapturePayment;
using JastipinAja.Payment.Features.CreatePayment;
using JastipinAja.Payment.Features.GetPaymentById;
using JastipinAja.Payment.Features.GetPayments;
using JastipinAja.Payment.Features.RefundPayment;
using JastipinAja.Payment.Features.ReleasePayment;
using JastipinAja.Payment.Persistence;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment
{
    public static class PaymentModuleServiceRegistrar
    {
        public static IServiceCollection AddPaymentModule(this IServiceCollection services, IConfiguration config) 
        {
            services.AddDbContext<PaymentDbContext>(opt =>
            opt.UseNpgsql(config.GetConnectionString("PaymentDb"),
                npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "payment")));

            services.AddScoped<RunningNumberGenerator<PaymentDbContext>>();
            services.AddScoped<CreatePaymentHandler>();
            services.AddScoped<CapturePaymentHandler>();
            services.AddScoped<ReleasePaymentHandler>();
            services.AddScoped<RefundPaymentHandler>();
            services.AddScoped<GetPaymentByIdHandler>();
            services.AddScoped<GetPaymentsHandler>();
            services.AddValidatorsFromAssembly(typeof(PaymentModuleServiceRegistrar).Assembly, includeInternalTypes: true);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PaymentModuleServiceRegistrar).Assembly));


            return services;

        }

        public static IEndpointRouteBuilder MapPaymentModule(this IEndpointRouteBuilder app)
        {
            app.MapOrderEndpoints();
            return app;
        }

    }
}
