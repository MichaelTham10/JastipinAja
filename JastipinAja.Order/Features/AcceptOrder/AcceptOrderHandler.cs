using JastipinAja.BuildingBlocks.Exceptions;
using JastipinAja.Order.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.AcceptOrder
{
    internal sealed class AcceptOrderHandler
    {
        private readonly OrderDbContext _db;
        public AcceptOrderHandler(OrderDbContext db) => _db = db;

        public async Task Handle(AcceptOrderCommand command, CancellationToken ct)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Order tidak ditemukan.");

            order.Accept();  // kalau status bukan Requested, entity sendiri yang menolak
            await _db.SaveChangesAsync(ct);
        }
    }
}
