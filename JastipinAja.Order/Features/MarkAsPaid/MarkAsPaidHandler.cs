using JastipinAja.BuildingBlocks.Exceptions;
using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.MarkAsPaid
{
    internal sealed class MarkAsPaidHandler
    {
        private readonly OrderDbContext _db;
        public MarkAsPaidHandler(OrderDbContext db) => _db = db;

        public async Task Handle(MarkAsPaidCommand command, CancellationToken ct)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Order tidak ditemukan.");

            order.MarkAsPaid();   // entity menolak kalau status bukan Accepted
            await _db.SaveChangesAsync(ct);
        }
    }
}
