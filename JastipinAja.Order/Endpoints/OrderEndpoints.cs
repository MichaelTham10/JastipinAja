using JastipinAja.Order.Features.CreateOrder;
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
        }
    }
}
