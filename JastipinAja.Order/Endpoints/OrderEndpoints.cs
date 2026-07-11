using JastipinAja.Order.Features.AcceptOrder;
using JastipinAja.Order.Features.CancelOrder;
using JastipinAja.Order.Features.CompleteOrder;
using JastipinAja.Order.Features.CreateOrder;
using JastipinAja.Order.Features.GetOrderById;
using JastipinAja.Order.Features.GetOrders;
using JastipinAja.Order.Features.MarkAsPaid;
using JastipinAja.Order.Features.MarkAsPurchased;
using JastipinAja.Order.Features.MarkAsReadyForHandover;
using JastipinAja.Order.Features.ShipOrder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace JastipinAja.Order.Endpoints
{
    internal static class OrderEndpoints
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/orders").WithTags("Orders");

            group.MapPost("/", async (CreateOrderRequest req, CreateOrderHandler handler, CancellationToken ct) =>
            {
                var result = await handler.Handle(
                    new CreateOrderCommand(req.CustomerId, req.ItemDescription, req.Price), ct);

                return Results.Created($"/orders/{result.PublicId}", result);
            });

            group.MapPost("/{id:guid}/cancel", async (Guid id, CancelOrderHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new CancelOrderCommand(id), ct);

                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/accept", async (Guid id, AcceptOrderHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new AcceptOrderCommand(id), ct);

                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/ship", async (Guid id, ShipOrderHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new ShipOrderCommand(id), ct);

                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/ready-handover", async (Guid id, MarkAsReadyForHandoverHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new MarkAsReadyForHandoverCommand(id), ct);

                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/complete", async (Guid id, CompleteOrderHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new CompleteOrderCommand(id), ct);

                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/pay", async (Guid id, MarkAsPaidHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new MarkAsPaidCommand(id), ct);
                return Results.NoContent();
            });

            group.MapPost("/{id:guid}/purchase", async (Guid id, MarkAsPurchasedHandler handler, CancellationToken ct) =>
            {
                await handler.Handle(new MarkAsPurchasedCommand(id), ct);
                return Results.NoContent();
            });

            group.MapGet("/{id:guid}", async (Guid id, GetOrderByIdHandler handler, CancellationToken ct) =>
            {
                var order = await handler.Handle(new GetOrderByIdQuery(id), ct);
                return order is null ? Results.NotFound() : Results.Ok(order);
            });

            group.MapGet("/", async (int? page, int? pageSize, GetOrdersHandler handler, CancellationToken ct) =>
            {
                var result = await handler.Handle(
                    new GetOrdersQuery(page ?? 1, pageSize ?? 20), ct);
                return Results.Ok(result);
            });

        }
    }
}
