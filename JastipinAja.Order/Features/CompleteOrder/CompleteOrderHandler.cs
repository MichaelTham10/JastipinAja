using JastipinAja.BuildingBlocks.Events;
using JastipinAja.BuildingBlocks.Exceptions;
using JastipinAja.Order.Features.MarkAsPaid;
using JastipinAja.Order.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.CompleteOrder
{
    internal sealed class CompleteOrderHandler
    {
        private readonly OrderDbContext _db;
        private readonly IPublisher _publisher;

        public CompleteOrderHandler(OrderDbContext db, IPublisher publisher) 
        {
            _db = db;
            _publisher = publisher;
        }

        public async Task Handle(CompleteOrderCommand command, CancellationToken ct)
        {
            var order = await _db.Orders
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Order tidak ditemukan.");

            order.Complete();   // entity menolak kalau status bukan Accepted
            await _db.SaveChangesAsync(ct);
            await _db.DispatchAndClearEvents(_publisher, ct); // baru publish SETELAH save sukses
        }
    }
}
