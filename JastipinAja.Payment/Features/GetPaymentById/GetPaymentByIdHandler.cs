using JastipinAja.Payment.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.GetPaymentById
{
    internal sealed class GetPaymentByIdHandler
    {
        private readonly PaymentDbContext _db;
        public GetPaymentByIdHandler(PaymentDbContext db) => _db = db;

        public async Task<PaymentDetailDto?> Handle(GetPaymentByIdQuery query, CancellationToken ct)
        {
            return await _db.Payments
                .AsNoTracking()
                .Where(o => o.PublicId == query.PublicId)
                .Select(o => new PaymentDetailDto(
                    o.PublicId,
                    o.PaymentNo,
                    o.Amount,
                    o.Status.ToString(),
                    o.CreatedAt))
                .FirstOrDefaultAsync(ct);
        }
    }
}
