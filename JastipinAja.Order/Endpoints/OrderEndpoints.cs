using JastipinAja.Order.Features.CreateOrder;
using JastipinAja.Order.Features.GetOrderById;
using JastipinAja.Order.Features.GetOrders;
using JastipinAja.Order.Features.MarkAsPaid;
using JastipinAja.Order.Features.MarkAsPurchased;
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
