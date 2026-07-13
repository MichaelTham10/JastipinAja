using JastipinAja.BuildingBlocks.Exceptions;
using JastipinAja.Payment.Features.CapturePayment;
using JastipinAja.Payment.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.RefundPayment
{

    internal sealed class RefundPaymentHandler
    {
        private readonly PaymentDbContext _db;
        public RefundPaymentHandler(PaymentDbContext db) => _db = db;

        public async Task Handle(RefundPaymentCommand command, CancellationToken ct)
        {
            var order = await _db.Payments
                .FirstOrDefaultAsync(o => o.PublicId == command.PublicId, ct)
                ?? throw new NotFoundException("Payment tidak ditemukan.");

            order.Refund();
            await _db.SaveChangesAsync(ct);
        }
    }
}
