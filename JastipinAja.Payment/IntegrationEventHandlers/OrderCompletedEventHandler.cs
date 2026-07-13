using JastipinAja.Order.Contracts.IntegrationEvents;
using JastipinAja.Payment.Features.ReleasePayment;
using JastipinAja.Payment.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.IntegrationEventHandlers
{
    internal sealed class OrderCompletedEventHandler : INotificationHandler<OrderCompletedEvent>
    {
        private readonly PaymentDbContext _db;
        private readonly ReleasePaymentHandler _releaseHandler;

        public OrderCompletedEventHandler(PaymentDbContext db, ReleasePaymentHandler releaseHandler)
        {
            _db = db;
            _releaseHandler = releaseHandler;
        }

        public async Task Handle(OrderCompletedEvent notification, CancellationToken ct)
        {
            var payment = await _db.Payments
                .FirstOrDefaultAsync(p => p.OrderPublicId == notification.OrderPublicId, ct);

            if (payment is null) return;   // tidak ada payment terkait, abaikan (atau log)

            await _releaseHandler.Handle(new ReleasePaymentCommand(payment.PublicId), ct);
        }
    }
}
