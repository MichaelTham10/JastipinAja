using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.GetOrderById
{
    internal sealed class GetOrderByIdHandler
    {
        private readonly OrderDbContext _db;
        public GetOrderByIdHandler(OrderDbContext db) => _db = db;

        public async Task<OrderDetailDto?> Handle(GetOrderByIdQuery query, CancellationToken ct)
        {
            return await _db.Orders
                .AsNoTracking()
                .Where(o => o.PublicId == query.PublicId)
                .Select(o => new OrderDetailDto(
                    o.PublicId,
                    o.OrderNo,
                    o.CustomerId,
                    o.ItemDescription,
                    o.Price,
                    o.Status.ToString(),
                    o.CreatedAt))
                .FirstOrDefaultAsync(ct);
        }
    }
}
