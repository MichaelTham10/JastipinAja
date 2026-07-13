using JastipinAja.Payment.Features.CapturePayment;
using JastipinAja.Payment.Features.CreatePayment;
using JastipinAja.Payment.Features.GetPaymentById;
using JastipinAja.Payment.Features.GetPayments;
using JastipinAja.Payment.Features.RefundPayment;
using JastipinAja.Payment.Features.ReleasePayment;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Endpoints
{
    internal static class PaymentEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/payments").WithTags("Payments");

            group.MapPost("/", async (CreatePaymentRequest req, CreatePaymentHandler handler, CancellationToken ct) =>
            {
                var result = await handler.Handle(
                    new CreatePaymentCommand(req.OrderPublicId, req.amount), ct);

                return Results.Created($"/payments/{result.PublicId}", result);
            });

            group.MapPost("/{id:guid}/capture", async (Guid id, CapturePaymentHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new CapturePaymentCommand(id), ct);

                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/release", async (Guid id, ReleasePaymentHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new ReleasePaymentCommand(id), ct);

                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/refund", async (Guid id, RefundPaymentHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new RefundPaymentCommand(id), ct);

                return Results.NoContent();
            });


            group.MapGet("/{id:guid}", async (Guid id, GetPaymentByIdHandler handler, CancellationToken ct) =>
            {
                var order = await handler.Handle(new GetPaymentByIdQuery(id), ct);
                return order is null ? Results.NotFound() : Results.Ok(order);
            });

            group.MapGet("/", async (int? page, int? pageSize, GetPaymentsHandler handler, CancellationToken ct) =>
            {
                var result = await handler.Handle(
                    new GetPaymentsQuery(page ?? 1, pageSize ?? 20), ct);
                return Results.Ok(result);
            });


        }

    }
}
