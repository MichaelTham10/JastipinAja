using JastipinAja.Payment.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.GetPayments
{
    internal sealed class GetPaymentsHandler
    {
        private readonly PaymentDbContext _db;
        public GetPaymentsHandler(PaymentDbContext db) => _db = db;

        public async Task<GetPaymentsResult> Handle(GetPaymentsQuery query, CancellationToken ct)
        {
            // penjaga: batasi pageSize supaya client tidak minta 1 juta sekaligus
            var page = query.Page < 1 ? 1 : query.Page;
            var pageSize = query.PageSize is < 1 or > 100 ? 20 : query.PageSize;

            var baseQuery = _db.Payments.AsNoTracking();

            var totalCount = await baseQuery.CountAsync(ct);

            var items = await baseQuery
                .OrderByDescending(o => o.CreatedAt)   // terbaru dulu
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new PaymentListItemDto(
                    o.PublicId,
                    o.PaymentNo,
                    o.Amount,
                    o.Status.ToString()))
                .ToListAsync(ct);

            return new GetPaymentsResult(items, page, pageSize, totalCount);
        }
    }
}
