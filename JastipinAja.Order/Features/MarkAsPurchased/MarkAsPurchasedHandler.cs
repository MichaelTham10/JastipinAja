using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.MarkAsPurchased
{
    internal sealed class MarkAsPurchasedHandler
    {
        private readonly OrderDbContext _db;
        public MarkAsPurchasedHandler(OrderDbContext db) => _db = db;

        public async Task Handle(MarkAsPurchasedCommand command, CancellationToken ct)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Order tidak ditemukan.");

            order.MarkAsPurchased();   // <- satu-satunya baris yang beda
            await _db.SaveChangesAsync(ct);
        }
    }
}
