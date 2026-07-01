using JastipinAja.Order.Features.AcceptOrder;
using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.ShipOrder
{
    internal sealed class ShipOrderHandler
    {
        private readonly OrderDbContext _db;
        public ShipOrderHandler(OrderDbContext db) => _db = db;

        public async Task Handle(AcceptOrderCommand command, CancellationToken ct)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Order tidak ditemukan.");

            order.Ship();  // kalau status bukan Requested, entity sendiri yang menolak
            await _db.SaveChangesAsync(ct);
        }
    }
}
