using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.GetOrders
{
    internal sealed class GetOrdersHandler
    {
        private readonly OrderDbContext _db;
        public GetOrdersHandler(OrderDbContext db) => _db = db;

        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken ct)
        {
            // penjaga: batasi pageSize supaya client tidak minta 1 juta sekaligus
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = query.PageSize is < 1 or > 100 ? 20 : query.PageSize;

            var baseQuery = _db.Orders.AsNoTracking();

            var totalCount = await baseQuery.CountAsync(ct);

            var items = await baseQuery
                .OrderByDescending(o => o.CreatedAt)   // terbaru dulu
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderListItemDto(
                    o.PublicId,
                    o.OrderNo,
                    o.ItemDescription,
                    o.Price,
                    o.Status.ToString()))
                .ToListAsync(ct);

            return new GetOrdersResult(items, page, pageSize, totalCount);
        }
    }
}
