using JastipinAja.BuildingBlocks.Exceptions;
using JastipinAja.Order.Features.MarkAsPurchased;
using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.MarkAsReadyForHandover
{

    internal sealed class MarkAsReadyForHandoverHandler
    {
        private readonly OrderDbContext _db;
        public MarkAsReadyForHandoverHandler(OrderDbContext db) => _db = db;

        public async Task Handle(MarkAsReadyForHandoverCommand command, CancellationToken ct)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Order tidak ditemukan.");

            order.MarkReadyForHandover();   // <- satu-satunya baris yang beda
            await _db.SaveChangesAsync(ct);
        }
    }
}
