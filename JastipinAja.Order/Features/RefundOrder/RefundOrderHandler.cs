using JastipinAja.Order.Features.MarkAsPaid;
using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.RefundOrder
{
    internal class RefundOrderHandler
    {
        private readonly OrderDbContext _db;
        public RefundOrderHandler(OrderDbContext db) => _db = db;

        public async Task Handle(MarkAsPaidCommand command, CancellationToken ct)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Order tidak ditemukan.");

            order.Refund();   // entity menolak kalau status bukan Accepted
            await _db.SaveChangesAsync(ct);
        }
    }
}
