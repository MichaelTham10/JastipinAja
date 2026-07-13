using JastipinAja.BuildingBlocks.Exceptions;
using JastipinAja.Payment.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.CapturePayment
{
    internal sealed class CapturePaymentHandler
    {
        private readonly PaymentDbContext _db;
        public CapturePaymentHandler(PaymentDbContext db) => _db = db;

        public async Task Handle(CapturePaymentCommand command, CancellationToken ct)
        {
            var order = await _db.Payments
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Payment tidak ditemukan.");

            order.Capture();  // kalau status bukan Requested, entity sendiri yang menolak
            await _db.SaveChangesAsync(ct);
        }
    }
}
